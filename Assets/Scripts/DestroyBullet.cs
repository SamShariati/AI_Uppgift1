using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public int playerHealth = 2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            
            Destroy(other.gameObject);
            playerHealth--;
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(playerHealth <= 0)
        {
            transform.position = new Vector3(0, -100, 0);
            
        }

        
    }
}
