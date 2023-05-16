using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Color color1 = Color.blue;
    private Color color2 = Color.cyan;

    public Camera cam;

    public float colourModifierFromX;

    void Start () {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;

        SnapCamera();
    }

    void Update () {
        GradientBackground();
    }

    void LateUpdate () {
        SmoothCameraFollow();
    }

    public void SnapCamera () {
        transform.position = target.position + offset;
    }

    private void GradientBackground() {
        if (transform.position.x < 0) {
            colourModifierFromX = 0;
        } else if (0 < transform.position.x && transform.position.x < 200) {
            colourModifierFromX = transform.position.x / 200;
        } else {
            colourModifierFromX = 1;
        }
        cam.backgroundColor = Color.Lerp(color1, color2, colourModifierFromX);
    }

    private void SmoothCameraFollow() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
