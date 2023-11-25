using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DT_Enemy : MonoBehaviour
{

    //Skriv vad som händer i alla tillstånd (koden för AI enemy).
    public abstract class TreeNode
    {
        
        public virtual void Execute(Transform transform, Transform Player)
        {
            
        }
    }
    
//-------------PATROL------------------------------------------------------
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

        public override void Execute(Transform enemy, Transform player)
        {
            Transform playerTest = player;
            Debug.Log("Patrolling");

            if (patrolPoints.Length > 0)
            {
                Vector3 targetPosition = patrolPoints[currentPointIndex].position;

                enemy.position = Vector3.MoveTowards(enemy.position, targetPosition, moveSpeed * Time.deltaTime);
                
                Vector3 direction = (targetPosition - enemy.position).normalized;
                //Quaternion lookRotation = Quaternion.LookRotation(direction);

                if( direction!= Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    enemy.rotation = Quaternion.Slerp(enemy.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }

                if (Vector3.Distance(enemy.position, targetPosition) < 0.1f)
                {
                    currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;

                }
               
            }
        }
        
    }



//-------------ATTACK---------------------------------------------------------
    public class Attack: TreeNode
    {
        float moveSpeed = 5f;
        float rotationSpeed = 5f;


        Transform bulletSpawnPoint;
        GameObject bulletPrefab;
        
        
        bool gunReady = true;
        float bulletCD = 3f;

        public Attack(GameObject bullet, Transform bulletSpawn)
        {
            bulletPrefab = bullet;
            bulletSpawnPoint = bulletSpawn;
        }

        public override void Execute(Transform enemy, Transform player)
        {
            Debug.Log("Attacking");

//------------------------MOVEMENT-----------------------------------------------------
            enemy.position = Vector3.MoveTowards(enemy.position, player.position, moveSpeed * Time.deltaTime);
            
            Vector3 direction = (player.position - enemy.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.rotation = Quaternion.Slerp(enemy.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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


//------------FLEE-------------------------------------------------------
    public class Flee: TreeNode
    {
        float moveSpeed = 0.15f;
        float rotationSpeed = 5f;

        Transform bulletSpawnPoint;
        GameObject bulletPrefab;

        bool gunReady = true;
        float bulletCD = 3f;
        public Flee(GameObject bullet, Transform bulletSpawn)
        {
            bulletPrefab = bullet;
            bulletSpawnPoint = bulletSpawn;
        }

        public override void Execute(Transform enemy, Transform player)
        {

//----------------------MOVEMENT----------------------------------
            Debug.Log("Fleeing");

            Vector3 directionToTarget = player.position - enemy.position;
            Vector3 newPosition = Vector3.Lerp(enemy.position, enemy.position - directionToTarget, moveSpeed * Time.deltaTime);

            // Assign the new position to the object
            enemy.position = newPosition;

            Vector3 direction = (player.position - enemy.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                enemy.rotation = Quaternion.Slerp(enemy.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }

            //------------------------SHOOTING-+-------------------------

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

//-----------DECISIONMAKER--------------------------------------------------

    public class DecisionMaker: TreeNode
    {
        public bool condition;
        public TreeNode trueBranch;
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
