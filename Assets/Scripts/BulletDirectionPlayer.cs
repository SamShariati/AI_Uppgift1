using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectionPlayer : MonoBehaviour
{
    public float bulletSpeed = 10;
    Rigidbody rbBullet;
    Vector3 direction;
    GameObject enemy;

    void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
        enemy = GameObject.Find("Enemy");
        SetDirection();
    }

    void SetDirection()
    {
        if (enemy != null)
        {
            direction.x = (enemy.transform.position.x - transform.position.x);
            direction.y = 0.5f;
            direction.z = (enemy.transform.position.z - transform.position.z);
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
