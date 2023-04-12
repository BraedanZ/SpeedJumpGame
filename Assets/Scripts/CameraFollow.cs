using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // void Awake () {
    //     transform.position = target.position + offset;
    // }

    void Start () {
        SnapCamera();
    }

    void LateUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SnapCamera () {
        transform.position = target.position + offset;
    }
}