using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform target;

    private GameMaster gm;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Color color1 = Color.blue;
    private Color color2 = Color.cyan;

    public Gradient skyGradient;

    public Camera cam;

    public float colourModifierFromX;

    void Start () {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        
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
        if (transform.position.x < gm.mapLength / 2) {
            FirstHalfGradient();
        } else {
            SecondHalfGradient();
        }
    }

    private void FirstHalfGradient() {
        if (transform.position.x < 0) {
            Debug.Log("from cam < 0");
            colourModifierFromX = 0 + 250 * (StaticClass.GetWorld() - 1);
        } else {
            Debug.Log("from cam");
            colourModifierFromX = (2 * transform.position.x + 250 * (StaticClass.GetWorld() - 1)) / gm.mapLength;
        }
        Debug.Log(colourModifierFromX);
        cam.backgroundColor = skyGradient.Evaluate(colourModifierFromX);
    }

    private void SecondHalfGradient() {
        if (transform.position.x > gm.mapLength) {
            colourModifierFromX = 0;
        } else {
            colourModifierFromX = 2 * ((gm.mapLength - transform.position.x) / gm.mapLength);
        }
        cam.backgroundColor = skyGradient.Evaluate(colourModifierFromX);
    }

    private void SmoothCameraFollow() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
