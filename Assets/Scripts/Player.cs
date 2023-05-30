using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Player player;

    Rigidbody2D rb;

    public Vector2 playerPosition;

    private BoxCollider2D playerCollider;

    private GameMaster gm;

    private AnimatePlayer animatePlayer;

    private new CameraFollow camera;

    public float verticalSpeed;
    public float horizontalSpeed;

    float pressSpaceTime = 0f;

    bool isGrounded;
    public LayerMask whatIsGround;

    bool isJumping;

    bool spacePressed;

    public Vector2 spawnOffset;

    private AudioController audioController;

    private bool hasLanded;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        animatePlayer = GameObject.FindGameObjectWithTag("Skin").GetComponent<AnimatePlayer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        transform.position = gm.GetRespawnPoint() + spawnOffset;
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
        Pause();
        DetectRInput();
        SetAnimation();
    }

    private void LocatePlayer() 
    {
        playerPosition = rb.transform.position;
        GroundCheck();
        if (rb.velocity.y <= 0) {
            isJumping = false;
        } else {
            isJumping = true;
        }
    }

    private void DetectSpaceInput() 
    {
        if (!gm.IsPaused()) {
            if (Input.GetKeyDown("space")) {
                spacePressed = true;
                audioController.PlayJumpStartSound();
            }

            if (Input.GetKeyUp("space")) {
                spacePressed = false;
                Jump();
            }
        }
    }

    private void Pause() {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape")) {
            if (gm.IsPaused()) {
                gm.UnpauseGame();
            } else {
                gm.PauseGame();
            }
        }
    }

    private void DetectRInput() 
    {
        if (Input.GetKeyDown("r") || Input.GetKeyDown("r")) {
            gm.Restart();
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
        CheckGroundedToJump();
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

    private void GroundCheck() {
        if (Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, -transform.up, 0.1f, whatIsGround)) {
            if (!isGrounded && !hasLanded) {
                audioController.PlayLandSound();
                hasLanded = true;
            }
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    private void CheckGroundedToJump() {
        if (isGrounded) {
            rb.AddForce(transform.right * horizontalSpeed * pressSpaceTime);
            rb.AddForce(transform.up * verticalSpeed * pressSpaceTime);
            audioController.PlayJumpEndSound();
            hasLanded = false;
            gm.IncramentJumps();
        }
    }

    private void StopJump() {
        if (isGrounded && !isJumping) {
            rb.velocity = Vector2.zero;
        }
    }    

    private void SetAnimation() {
        if (isGrounded && !spacePressed) {
            animatePlayer.AnimateBase();
        } else if (isGrounded && spacePressed) {
            animatePlayer.AnimateSquish();
        } else {
            animatePlayer.AnimateJump();
        }
    }

    public void Die() {
        gm.IncramentDeath();
        audioController.PlayDieSound();
        transform.position = gm.GetRespawnPoint() + spawnOffset;
        rb.velocity = Vector2.zero;
        camera.SnapCamera();
    }
}
