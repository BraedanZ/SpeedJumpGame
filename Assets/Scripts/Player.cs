using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Player player;

    Rigidbody2D rb;

    public Vector2 playerPosition;

    public float verticalSpeed;
    public float horizontalSpeed;

    float pressSpaceTime;
    float unPressSpaceTime;
    float duration;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = rb.transform.position;

        if (Input.GetKeyDown("space")) {
            print("space was pressed");
            pressSpaceTime = Time.time;
        }

        if (Input.GetKeyUp("space")) {
            print("space was released");
            unPressSpaceTime = Time.time;
            duration = unPressSpaceTime - pressSpaceTime;
            rb.AddForce(transform.right * horizontalSpeed * duration);
            rb.AddForce(transform.up * verticalSpeed * duration);
        }
    }
}
