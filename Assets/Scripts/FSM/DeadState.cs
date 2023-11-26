using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyBaseState
{

    double timeUntilDead = 2.5;
    bool dead = false;
    public override void EnterState(StateManager enemy)
    {
        Debug.Log("Hello From the Dead State");
    }

    public override void UpdateState(StateManager enemy)
    {
        Debug.Log("Dead");

        timeUntilDead -= Time.deltaTime;

        if(timeUntilDead < 0)
        {
            if (!dead)
            {
                dead = true;
                enemy.KillMe();
            }
        }

    }
}
