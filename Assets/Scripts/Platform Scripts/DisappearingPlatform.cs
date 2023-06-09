using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("ActivatePlatform", 2.0f, 4.0f);
        InvokeRepeating("DeactivatePlatform", 0.0f, 4.0f);
    }

    private void ActivatePlatform() {
        this.gameObject.SetActive(true);
    }

    private void DeactivatePlatform() {
        this.gameObject.SetActive(false);
    }
}
