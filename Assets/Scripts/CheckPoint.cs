using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private GameMaster gm;

    private BoxCollider2D checkpointCollider;

    public LayerMask whatIsPlayer;

    public float maxDistance;

    public Vector3 boxSize;

    void Start() {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        checkpointCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        CheckForPlayer();
    }

    // void OnTriggerEnter2D(Collider2D other) {
    //     if (other.CompareTag("Player")) {
    //         gm.AddCheckPoint(transform.position);
    //     }
    // }

    public void CheckForPlayer() {
        if (Physics2D.BoxCast(checkpointCollider.bounds.center, checkpointCollider.bounds.size, 0, transform.up, 0.1f, whatIsPlayer)) {
            gm.AddCheckPoint(transform.position);
        }
    }

    // void OnDrawGizmos() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(transform.position + transform.up * maxDistance, checkpointCollider.bounds.size);
    // }
}
