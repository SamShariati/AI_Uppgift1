using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : EnemyBaseState
{
    float playerDistance;
    float moveSpeed = 0.15f;
    float rotationSpeed = 5f;
    bool gunReady = true;
    float bulletCD = 3f;
    public override void EnterState(StateManager enemy)
    {
        Debug.Log("Hello From the Flee State");
    }

    public override void UpdateState(StateManager enemy)
    {
        playerDistance = Vector3.Distance(enemy.transform.position, enemy.playerObject.position);
        if (enemy.enemyHealth <= 0)
        {
            enemy.SwitchState(enemy.deadState);
        }
        else if (playerDistance > 100)
        {
            enemy.SwitchState(enemy.patrolState);
        }
        else
        {
            Debug.Log("Fleeing");

            Vector3 directionToTarget = enemy.playerObject.position - enemy.transform.position;
            Vector3 newPosition = Vector3.Lerp(enemy.transform.position, enemy.transform.position - directionToTarget, moveSpeed * Time.deltaTime);

            // Assign the new position to the object
            enemy.transform.position = newPosition;

            Vector3 direction = (enemy.playerObject.position - enemy.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            //------------------------SHOOTING-+-------------------------

            if (!gunReady)
            {
                bulletCD -= Time.deltaTime;

                if (bulletCD <= 0)
                {
                    gunReady = true;
                }
            }

            if (gunReady)
            {
                gunReady = false;
                enemy.GenerateBullet();
                bulletCD = 3;

            }
        }
    }

    //public override void OnTriggerEnter(StateManager enemy, Collider collider)
    //{

    //}
}
