using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{

    public Gradient skyGradient;

    public Camera cam;

    public float colourModifier;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void Update()
    {
        GradientBackground();
    }

    private void GradientBackground() {
        colourModifier = Mathf.PingPong(Time.time / 50, 1);
        cam.backgroundColor = skyGradient.Evaluate(colourModifier);
    }
}
