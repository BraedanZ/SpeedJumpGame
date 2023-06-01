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

    public Gradient skyGradient;

    public Camera cam;

    public float mapLength;

    public float colourModifierFromX;

    void Start () {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;

        SnapCamera();
    }

    void Update () {
        GradientBackground();
    }

    void FixedUpdate () {
        SmoothCameraFollow();
    }

    public void SnapCamera () {
        transform.position = target.position + offset;
    }

    private void GradientBackground() {
        if (transform.position.x < mapLength / 2) {
            FirstHalfGradient();
        } else {
            SecondHalfGradient();
        }
    }

    private void FirstHalfGradient() {
        if (transform.position.x < 0) {
            colourModifierFromX = 0;
        } else {
            colourModifierFromX = 2 * transform.position.x / mapLength;
        }
        cam.backgroundColor = skyGradient.Evaluate(colourModifierFromX);
    }

    private void SecondHalfGradient() {
        if (transform.position.x > mapLength) {
            colourModifierFromX = 0;
        } else {
            colourModifierFromX = 2 * ((mapLength - transform.position.x) / mapLength);
        }
        cam.backgroundColor = skyGradient.Evaluate(colourModifierFromX);
    }

    private void SmoothCameraFollow() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
