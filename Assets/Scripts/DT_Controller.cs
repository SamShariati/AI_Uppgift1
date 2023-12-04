using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Controller : MonoBehaviour
{
    public Transform player;
    bool lowHealth=false;
    bool playerInVision=false;
    DT_Enemy.DecisionMaker root;
    DT_Enemy.DecisionMaker trueNode;
    public Transform[] patrolPoints;
    public int enemyHealth = 10;
    
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float playerDistance;


    void Start()
    {
        //--------------TRUENODE: (LOWHEALTH = FALSE, TRUE: FLEE STATE, FALSE= ATTACK STATE)
        trueNode = new DT_Enemy.DecisionMaker(lowHealth, new DT_Enemy.Flee(bulletPrefab, bulletSpawnPoint), new DT_Enemy.Attack(bulletPrefab, bulletSpawnPoint));

        //--------------ROOT: (PLAYERINVISION=FALSE, TRUE = TRUENODE, FALSE= PATROL STATE)
        root = new DT_Enemy.DecisionMaker(playerInVision, trueNode, new DT_Enemy.Patrol(patrolPoints));


        //root = new DT_Enemy.DecisionMaker(
        //playerInVision,
        //new DT_Enemy.DecisionMaker(lowHealth, //True Branch
        //new DT_Enemy.Flee(), //True branch för decisionmaker
        //new DT_Enemy.Attack(bulletPrefab, bulletSpawnPoint)), //false branch för desicionmaker
        //new DT_Enemy.Patrol(patrolPoints) //sista parametern i root som ska vara en false branch= Flee
        //);
    }

    private void Update()
    {

        playerDistance = Vector3.Distance(transform.position, player.position);
        if(playerDistance > 100)
        {
            playerInVision = false;
        }
        else if(playerDistance < 25)
        {
            playerInVision = true;
        }

        if(enemyHealth <= 5)
        {
            lowHealth = true;
        }


        root.Execute(transform, player);
        root.condition = playerInVision;


        trueNode.condition = lowHealth;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {

            Destroy(other.gameObject);
            enemyHealth--;
        }

    }

   
}
