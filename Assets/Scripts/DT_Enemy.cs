using UnityEngine;

public class DT_Enemy : MonoBehaviour
{

    //Skriv vad som händer i alla tillstånd (koden för AI enemy).
    public abstract class TreeNode
    {
        
        public virtual void Execute(Transform transform, Transform Player)
        {
            
        }
    }
    

    public class Patrol : TreeNode
    {

        Transform[] patrolPoints;
        float moveSpeed = 15f;
        int currentPointIndex = 0;
        float rotationSpeed = 5f;
        

        Transform transform;

        public Patrol(Transform[] patrolPoints)
        {

            this.patrolPoints = patrolPoints;
            
        }

        public override void Execute(Transform transform, Transform player)
        {
            Transform playerTest = player;
            Debug.Log("Patrolling");

            if (patrolPoints.Length > 0)
            {
                Vector3 targetPosition = patrolPoints[currentPointIndex].position;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                
                Vector3 direction = (targetPosition - transform.position).normalized;
                //Quaternion lookRotation = Quaternion.LookRotation(direction);

                if( direction!= Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }

                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
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
        float moveSpeed = 5f;
        float rotationSpeed = 5f;


        Transform bulletSpawnPoint;
        GameObject bulletPrefab;
        float bulletSpeed = 10;
        Vector3 direction;
        bool gunReady = true;
        float bulletCD = 3f;

        public override void Execute(Transform transform, Transform player)
        {
            Debug.Log("Attacking");

//------------------------MOVEMENT-----------------------------------------------------
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletCD = 3;

            }
        }
    }

    public class Flee: TreeNode
    {
        public override void Execute(Transform transform, Transform Player)
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

        public override void Execute(Transform transform, Transform Player)
        {
            if(condition)
            {

                trueBranch.Execute(transform, Player);

            }
            else
            {
                falseBranch.Execute(transform, Player);
            }
            
        }
    }
}
