using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Controller : MonoBehaviour
{
    public bool playerInRange;
    DT_Enemy.DecisionMaker root;

    
    void Start()
    {
        
        root = new DT_Enemy.DecisionMaker(
        playerInRange,
        new DT_Enemy.DecisionMaker(false, //True Branch
        new DT_Enemy.Attack(), //True branch för decisionmaker
        new DT_Enemy.Flee()), //false branch för desicionmaker
        new DT_Enemy.Patrol() //sista parametern i root som ska vara en false branch= Flee
        );
    }

    private void Update()
    {
        root.Execute();
        root.condition = playerInRange;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange=false;
        }
    }
}
