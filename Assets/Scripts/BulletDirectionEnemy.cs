using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionEnemy : MonoBehaviour
{
    public float bulletSpeed = 10;
    Rigidbody rbBullet;
    Vector3 direction;
    GameObject player;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        SetDirection();
    }

    void SetDirection()
    {
        if (player != null)
        {
            direction.x = (player.transform.position.x - transform.position.x);
            direction.y = 0.5f;
            direction.z = (player.transform.position.z - transform.position.z);
            direction = direction.normalized;
        }
        else
        {
            // Handle the case where the player object is not found
            direction = Vector3.forward; // Default direction, change it as needed
        }
    }

    void Update()
    {
        rbBullet.velocity = direction * bulletSpeed;

        if (transform.position.x > 60 || transform.position.x < -60 || transform.position.z > 60 || transform.position.z < -60)
        {
            Destroy(this.gameObject);
        }
    }
}
