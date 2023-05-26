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

    private Color morningPink = new Color(.984f, .569f, .561f, 1f);
    private Color morningTransition = new Color(.984f, .647f, .545f, 1f);
    private Color morningOrange = new Color(1f, .831f, .592f, 1f);
    private Color earlyBlue = new Color(.831f, .980f, 980f, 1f);
    private Color midBlue = new Color(.765f, .933f, .980f, 1f);
    private Color lateBlue = new Color(.686f, .835f, .941f, 1f);

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
        cam.backgroundColor = Color.Lerp(morningOrange, earlyBlue, colourModifierFromX);
    }

    private void SmoothCameraFollow() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
