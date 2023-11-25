using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float rotationSpeed = 300f;
    public Transform enemy;

    bool gunReady = true;
    float bulletCD = 3f;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Calculate rotation towards the movement direction
        //if (movement.magnitude >= 0.1f)
        //{
        //    Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        //}

        // Move the object
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        Vector3 direction = (enemy.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

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
