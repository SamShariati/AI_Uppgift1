using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
   
    public abstract void EnterState(StateManager enemy);

    public abstract void UpdateState(StateManager enemy);

    //public abstract void OnTriggerEnter(StateManager enemy, Collider collider);
    
}
