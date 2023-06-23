using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPlatform : MonoBehaviour
{
    public GameObject killZone;

    private SpriteRenderer sprite;

    private bool isFlipped;

    void Start()
    {
        killZone = this.gameObject.transform.GetChild(0).gameObject;
        sprite = killZone.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("ActivateKillZone", 2.0f, 4.0f);
        InvokeRepeating("DeactivateKillZone", 0.0f, 4.0f);
        InvokeRepeating("FlipLightning", 0.0f, 0.5f);

    }

    private void ActivateKillZone() {
        killZone.SetActive(true);
    }

    private void DeactivateKillZone() {
        killZone.SetActive(false);
    }

    private void FlipLightning() {
        if (isFlipped) {
            isFlipped = false;
            sprite.flipX = false;
        } else {
            isFlipped = true;
            sprite.flipX = true;
        }
    }
}
