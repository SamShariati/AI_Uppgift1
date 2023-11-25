using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    public Transform player;
    public Transform enemy;


    public Vector3 cameraPos;

    float averageDistance;
    public float minFOV = 50f;
    public float maxFOV = 60f;
    float zoom;

    private Camera cam;

    float distance;


    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {

        distance = Vector3.Distance(player.position, enemy.position);

        averageDistance = distance / 2f;

        zoom = Mathf.InverseLerp(0f, 120f, averageDistance);
        cam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, zoom * 5);


        cameraPos = new Vector3(((player.position.x + enemy.position.x) / 2),
            40,
            ((player.position.z + enemy.position.z) / 2) - 15);

        transform.position = cameraPos;


    }
}
