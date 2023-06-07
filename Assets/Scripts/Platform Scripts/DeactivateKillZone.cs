using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateKillZone : MonoBehaviour
{
    private BoxCollider2D deactivatorCollider;

    public LayerMask whatIsPlayer;

    public GameObject killZone;

    void Start()
    {
        deactivatorCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer() {
        if (Physics2D.BoxCast(deactivatorCollider.bounds.center, deactivatorCollider.bounds.size, 0, transform.up, 0.1f, whatIsPlayer)) {
            killZone.SetActive(false);
        }
    }
}
