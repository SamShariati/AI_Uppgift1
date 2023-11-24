using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTest : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 5f;
    int currentPointIndex = 0;


   

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {

        if(patrolPoints.Length > 0)
        {

            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);
            Vector3 direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            if(Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

            }
            else
            {
                Debug.Log("No patrol points assigned");
            }
        }
    }
}
