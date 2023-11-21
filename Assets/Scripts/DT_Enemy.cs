using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DT_Enemy : MonoBehaviour
{

    //Skriv vad som händer i alla tillstånd (koden för AI enemy).
    public abstract class TreeNode
    {
        public virtual void Execute()
        {
            
        }
    }
    

    public class Patrol : TreeNode
    {
        public override void Execute()
        {
            Debug.Log("Patrolling");
        }
        
    }

    public class Attack: TreeNode
    {
        public override void Execute()
        {
            Debug.Log("Attacking");
        }
    }

    public class Flee: TreeNode
    {
        public override void Execute()
        {
            Debug.Log("Fleeing");
        }
    }

    public class DecisionMaker: TreeNode
    {
        public bool condition;
        TreeNode trueBranch;
        TreeNode falseBranch;

        public DecisionMaker(bool condition, TreeNode trueBranch, TreeNode falseBranch)
        {
            this.condition = condition;
            this.trueBranch = trueBranch;
            this.falseBranch = falseBranch;
        }

        public override void Execute()
        {
            if(condition)
            {

                trueBranch.Execute();

            }
            else
            {
                falseBranch.Execute();
            }
            
        }
    }
}
