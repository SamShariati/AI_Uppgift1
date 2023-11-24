using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Controller : MonoBehaviour
{
    public Transform player;
    public bool wtf;
    public bool playerInVision;
    DT_Enemy.DecisionMaker root;
    public Transform[] patrolPoints;
    int hitpoints = 10;


    void Start()
    {
        
        root = new DT_Enemy.DecisionMaker(
        playerInVision,
        new DT_Enemy.DecisionMaker(true, //True Branch
        new DT_Enemy.Attack(), //True branch för decisionmaker
        new DT_Enemy.Flee()), //false branch för desicionmaker
        new DT_Enemy.Patrol(patrolPoints) //sista parametern i root som ska vara en false branch= Flee
        );
    }

    private void Update()
    {
        root.Execute(transform, player);
        root.condition = playerInVision;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInVision = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInVision=false;
    //    }
    //}
}
