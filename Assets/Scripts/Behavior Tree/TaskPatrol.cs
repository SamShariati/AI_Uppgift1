using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TaskPatrol : Node
{

    private Transform _transform;
    private Transform[] _wayPoints;
    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    
    public TaskPatrol(Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _wayPoints=wayPoints;
    }

    public override NodeState Evaluate()
    {
        if(_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter < _waitTime)
            {
                _waiting = false;
            }
            
        }
        else
        {
            Transform wp = _wayPoints[_currentWaypointIndex];

            if (Vector3.Distance(_transform.position, wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
        }

        state = NodeState.Running;
        return state;

        
    }

}
