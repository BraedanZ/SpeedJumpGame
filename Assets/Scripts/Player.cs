using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Player player;

    Rigidbody2D rb;

    public Vector2 playerPosition;

    private GameMaster gm;

    private new CameraFollow camera;

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
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        Vector2 spawnOffset = new Vector2(-1.60f, 1.9f);
        transform.position = gm.GetTopCheckPoint() + spawnOffset;
        camera.SnapCamera();
    }

    void FixedUpdate() 
    {
        LocatePlayer();
        AddJumpTime();
        StopJump();
    }

    void Update() {
        DetectSpaceInput();
        DetectShiftInput();
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

    private void DetectSpaceInput() 
    {
        if (Input.GetKeyDown("space")) {
            spacePressed = true;
        }

        if (Input.GetKeyUp("space")) {
            spacePressed = false;
            Jump();
        }
    }

    private void DetectShiftInput() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void AddJumpTime() {
        if (spacePressed) {
            pressSpaceTime += Time.deltaTime;
        }
    }

    private void Jump()
    {
        LimitJump();
        CheckGrounded();
        pressSpaceTime = 0f;
    }

    private void LimitJump() {
        if (pressSpaceTime > 0.8f) {
            pressSpaceTime = 0.8f;
        }

        if (pressSpaceTime < 0.15f) {
            pressSpaceTime = 0.15f;
        }
    }

    private void CheckGrounded() {
        if (isGrounded) {
            rb.AddForce(transform.right * horizontalSpeed * pressSpaceTime);
            rb.AddForce(transform.up * verticalSpeed * pressSpaceTime);
        }
    }

    private void StopJump() {
        if (isGrounded && !isJumping) {
            rb.velocity = Vector2.zero;
        }
    }    
}
