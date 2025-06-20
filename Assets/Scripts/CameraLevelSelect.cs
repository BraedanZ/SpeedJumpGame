using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevelSelect : MonoBehaviour
{
    public Gradient skyGradient;

    public Camera cam;
    public float colourModifierFromX;
    private int world;
    public void SetBG()
    {
        world = StaticClass.GetWorld();
        if (world == 1)
        {
            colourModifierFromX = 0.014f;
        } else if (world == 2) {
            colourModifierFromX = 0.139f;
        } else if (world == 3) {
            colourModifierFromX = 0.264f;
        } else if (world == 4) {
            colourModifierFromX = 0.389f;
        } else if (world == 5) {
            colourModifierFromX = 0.514f;
        } else if (world == 6) {
            colourModifierFromX = 0.639f;
        } else if (world == 7) {
            colourModifierFromX = 0.764f;
        } else if (world == 8) {
            colourModifierFromX = 0.889f;
        }
        
        cam.backgroundColor = skyGradient.Evaluate(colourModifierFromX);
    }
}
