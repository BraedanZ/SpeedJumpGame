using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlatform : MonoBehaviour
{
    public GameObject killZone;

    void Start()
    {
        killZone = this.gameObject.transform.GetChild(0).gameObject;
        InvokeRepeating("ActivateKillZone", 2.0f, 4.0f);
        InvokeRepeating("DeactivateKillZone", 0.0f, 4.0f);
    }

    private void ActivateKillZone() {
        killZone.SetActive(true);
    }

    private void DeactivateKillZone() {
        killZone.SetActive(false);
    }
}
