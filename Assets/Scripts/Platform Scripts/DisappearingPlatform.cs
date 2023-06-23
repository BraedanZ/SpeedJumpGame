using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    private bool isActive;

    private Renderer renderer;

    // private Color fadedColour;
    // private Color unFadedColour;

    private Color lerpedColor = Color.white;

    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        InvokeRepeating("ActivatePlatform", 2.0f, 4.0f);
        InvokeRepeating("DeactivatePlatform", 0.0f, 4.0f);
    }

    void Update() {
        // lerpedColor = Color.Lerp(new Color(255, 255, 255, 255), new Color(255, 255, 255, 0), Mathf.PingPong(Time.time, 1));
        // renderer.material.color = lerpedColor;
    }

    private void ActivatePlatform() {
        isActive = true;
        this.gameObject.SetActive(true);
    }

    private void DeactivatePlatform() {
        isActive = false;
        this.gameObject.SetActive(false);
    }
}
