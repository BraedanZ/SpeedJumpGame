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

    bool isGrounded;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isJumping;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition = rb.transform.position;

        isGrounded = (Physics2D.OverlapCircle(groundCheck1.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround));

        if (rb.velocity.y <= 0) {
            isJumping = false;
        } else {
            isJumping = true;
        }

        if (Input.GetKeyDown("space")) {
            pressSpaceTime = Time.time;
        }

        if (Input.GetKeyUp("space")) {
            unPressSpaceTime = Time.time;
            duration = unPressSpaceTime - pressSpaceTime;

            print(duration);
            if(duration > 0.8f) {
                duration = 0.8f;
            }
            print(duration);

            if (isGrounded) {
                rb.AddForce(transform.right * horizontalSpeed * duration);
                rb.AddForce(transform.up * verticalSpeed * duration);
            }
        }

        if (isGrounded && !isJumping) {
            rb.velocity = Vector2.zero;
        }
    }

    
}
