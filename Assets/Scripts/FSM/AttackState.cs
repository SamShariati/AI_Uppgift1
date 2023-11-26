using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{

    Transform bulletSpawnPoint;
    GameObject bulletPrefab;

    float moveSpeed = 5f;
    float rotationSpeed = 5f;
    bool gunReady = true;
    float bulletCD = 3f;
    float playerDistance;
    
    
    public override void EnterState(StateManager enemy)
    {

        
        Debug.Log("Hello From the Attack State");

        bulletSpawnPoint = enemy.bulletSpawnPoint;
        bulletPrefab = enemy.bulletPrefab;
        
    }

    public override void UpdateState(StateManager enemy)
    {

        playerDistance = Vector3.Distance(enemy.transform.position, enemy.playerObject.position);
        if (enemy.enemyHealth < 5)
        {
            enemy.SwitchState(enemy.fleeState);
        }
        else if(playerDistance > 100)
        {
            enemy.SwitchState(enemy.patrolState);
        }
        else
        {
            Debug.Log("Attacking");

            //------------------------MOVEMENT-----------------------------------------------------
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.playerObject.position, moveSpeed * Time.deltaTime);

            Vector3 direction = (enemy.playerObject.position - enemy.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            //-----------------------SHOOTING------------------------------------------------------------

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
    //    GameObject bullet = collider.gameObject;
    //    if (bullet.CompareTag("PlayerBullet"))
    //    {
    //        Destroy(bullet.gameObject);
    //        enemyHealth--;
    //    }
    //}
}

