using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    EnemyBaseState currentState;

    public PatrolState patrolState = new PatrolState();
    public AttackState attackState = new AttackState();
    public FleeState fleeState = new FleeState();
    public DeadState deadState = new DeadState();

    public int enemyHealth = 10;

    //------------PatrolState--------------------

    public Transform playerObject;
    public Transform[] patrolPoints;

    //-----------AttackState-------------------

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    void Start()
    {
        currentState = patrolState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        //currentState.OnTriggerEnter(this, collider);
        if (collider.CompareTag("PlayerBullet"))
        {

            Destroy(collider.gameObject);
            enemyHealth--;
        }

    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void GenerateBullet()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
    }

    public void KillMe()
    {
        Destroy(transform.gameObject);
    }
}
