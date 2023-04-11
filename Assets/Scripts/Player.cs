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

    float pressSpaceTime = 0f;

    bool isGrounded;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isJumping;

    bool spacePressed;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    {
        LocatePlayer();
        AddJumpTime();
        StopJump();
    }

    void Update() {
        DetectInput();
    }

    private void LocatePlayer() 
    {
        playerPosition = rb.transform.position;
        isGrounded = (Physics2D.OverlapCircle(groundCheck1.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround));
        if (rb.velocity.y <= 0) {
            isJumping = false;
        } else {
            isJumping = true;
        }
    }

    private void DetectInput() 
    {
        if (Input.GetKeyDown("space")) {
            spacePressed = true;
        }

        if (Input.GetKeyUp("space")) {
            spacePressed = false;
            Jump();
        }
    }

    private void AddJumpTime() {
        if (spacePressed) {
            pressSpaceTime += Time.deltaTime;
        }
    }

    private void Jump()
    {
        print(pressSpaceTime);
        if(pressSpaceTime > 0.8f) {
            pressSpaceTime = 0.8f;
        }
        print(pressSpaceTime);

        if (isGrounded) {
            rb.AddForce(transform.right * horizontalSpeed * pressSpaceTime);
            rb.AddForce(transform.up * verticalSpeed * pressSpaceTime);
        }

        pressSpaceTime = 0f;
    }

    private void StopJump() {
        if (isGrounded && !isJumping) {
            rb.velocity = Vector2.zero;
        }
    }    
}
