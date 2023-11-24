using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    Vector3 direction;
    bool gunReady=true;
    float bulletCD = 3f;
    // Start is called before the first frame update

    private void Update()
    {
        if(!gunReady)
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
    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
            //var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
