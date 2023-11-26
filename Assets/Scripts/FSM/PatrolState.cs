using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{


    Transform[] patrolPoints;
    float moveSpeed = 15f;
    int currentPointIndex = 0;
    float rotationSpeed = 5f;
    float playerDistance;



    public override void EnterState(StateManager enemy)
    {
        patrolPoints = enemy.patrolPoints;
        Debug.Log("Hello From the Patrol State");
        
    }

    public override void UpdateState(StateManager enemy)
    {
        playerDistance = Vector3.Distance(enemy.transform.position, enemy.playerObject.position);


        //-----------------KONTROLLERAR IFALL DEN SKA PATRULLERA ELLER INTE (TRUE=byt till attack mode, false=patrullera)-------------
        if (playerDistance < 25)
        {
            enemy.SwitchState(enemy.attackState);
        }
        else
        {
            Debug.Log("Patrolling");

            //------------PATRULLERAR KRING PUNKTERNA--------------------------------------
            if (patrolPoints.Length > 0)
            {
                Vector3 targetPosition = patrolPoints[currentPointIndex].position;

                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);


                //---------ROTERAR OBJEKTET TILL RÄTT HÅLL----------------------------------
                Vector3 direction = (targetPosition - enemy.transform.position).normalized;

                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }

                //------------BYTER PATRULLPUNKT TILL NÄSTA NÄR DEN HAR NÅTT FRAM TILL SIN URSPRUNGLIGA------------------
                if (Vector3.Distance(enemy.transform.position, targetPosition) < 0.1f)
                {
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

                }

            }
        }
        
    }

    //public override void OnTriggerEnter(StateManager enemy, Collider collider)
    //{

    //}
}
