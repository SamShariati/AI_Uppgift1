using UnityEngine;

public class DT_Enemy : MonoBehaviour
{

    //Skriv vad som händer i alla tillstånd (koden för AI enemy).
    public abstract class TreeNode
    {
        public virtual void Execute(Transform transform)
        {
            
        }
    }
    

    public class Patrol : TreeNode
    {

        Transform[] patrolPoints;
        float moveSpeed = 15f;
        int currentPointIndex = 0;
        

        Transform transform;

        public Patrol(Transform[] patrolPoints)
        {

            this.patrolPoints = patrolPoints;
            //this.transform = transform;
        }

        public override void Execute(Transform transform)
        {
            Debug.Log("Patrolling");

            if (patrolPoints.Length > 0)
            {

                transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);
                Vector3 direction = (patrolPoints[currentPointIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) < 0.1f)
                {
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

                }
                else
                {
                    Debug.Log("No patrol points assigned");
                }
            }
        }
        
    }

    public class Attack: TreeNode
    {
        public override void Execute(Transform transform)
        {
            Debug.Log("Attacking");
        }
    }

    public class Flee: TreeNode
    {
        public override void Execute(Transform transform)
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

        public override void Execute(Transform transform)
        {
            if(condition)
            {

                trueBranch.Execute(transform);

            }
            else
            {
                falseBranch.Execute(transform);
            }
            
        }
    }
}
