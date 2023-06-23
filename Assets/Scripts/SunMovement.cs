using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public GameObject cam;

    private Vector3 startPosition;

    private Vector3 lerpedPosition;
    private float lerpedX;
    private float lerpedY;

    void Start()
    {
        startPosition = new Vector3(-11.6f, 2.2f, 0f);
    }

    void Update()
    {
        lerpedX = cam.transform.position.x;
        lerpedY = (-0.00002f) * Mathf.Pow((lerpedX - 1000), 2f);
        lerpedPosition.x = lerpedX - 25.6f + (51 * (cam.transform.position.x / 2000f));
        lerpedPosition.y = lerpedY + 15f /*+ cam.transform.position.y*/;
        lerpedPosition.z = 0f;
        transform.position = lerpedPosition;
    }
}



