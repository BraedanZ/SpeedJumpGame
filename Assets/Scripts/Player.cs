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

    public float maxDistance;

    private int skinSelector;

    public float xToMaxXRatio;

    private bool canJump = true;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        animatePlayer = GameObject.FindGameObjectWithTag("Skin").GetComponent<AnimatePlayer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        // maxDistance = transform.position.x;
        SelectSkin();
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

    private void SetMaxDistance() {
        if (transform.position.x > maxDistance) {
            maxDistance = transform.position.x;
        }
    }

    private void DetectSpaceInput() 
    {
        if (!gm.IsPaused()) {
            if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Mouse0)) {
                spacePressed = true;
                audioController.PlayJumpStartSound();
                canJump = true;
            }

            if (Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.Mouse0)) {
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
        if (canJump) {
            if (spacePressed) {
                pressSpaceTime += Time.deltaTime;
                if (pressSpaceTime > 0.8f) {
                    Jump();
                }
            }
        }
    }

    private void Jump()
    {
        if (canJump) {
            LimitJump();
            CheckGroundedToJump();
            pressSpaceTime = 0f;
        }
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
            SelectSkin();
        } else {
            isGrounded = false;
        }
    }

    private void SelectSkin() {
        if (transform.position.x > 0) {
            xToMaxXRatio = transform.position.x / maxDistance;
        }

        if (xToMaxXRatio <= 0) {
            skinSelector = 4;
        } else if (xToMaxXRatio > 0f && xToMaxXRatio <= 0.15f) {
            skinSelector = 0;
        } else if (xToMaxXRatio > 0.15f && xToMaxXRatio <= 0.3f) {
            skinSelector = 1;
        } else if (xToMaxXRatio > 0.3f && xToMaxXRatio <= 0.45f) {
            skinSelector = 2;
        } else if (xToMaxXRatio > 0.45f && xToMaxXRatio <= 0.6f) {
            skinSelector = 3;
        } else if (xToMaxXRatio > 0.6f && xToMaxXRatio <= 0.75f) {
            skinSelector = 4;
        } else if (xToMaxXRatio > 0.75f && xToMaxXRatio <= 0.9f) {
            skinSelector = 5;
        } else if (xToMaxXRatio > 0.9f && xToMaxXRatio <= 1.05f) {
            skinSelector = 6;
        } else if (xToMaxXRatio > 1.05f && xToMaxXRatio <= 1.2f) {
            skinSelector = 7;
        } else {
            skinSelector = 8;
        }
    }

    private void CheckGroundedToJump() {
        if (isGrounded) {
            rb.AddForce(transform.right * horizontalSpeed * pressSpaceTime);
            rb.AddForce(transform.up * verticalSpeed * pressSpaceTime);
            canJump = false;
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
            animatePlayer.AnimateBase(skinSelector);
        } else if (isGrounded && spacePressed && canJump) {
            animatePlayer.AnimateSquish();
        } else if (isGrounded && spacePressed && !canJump) {
            animatePlayer.AnimateBase(skinSelector);
            } else {
            animatePlayer.AnimateJump(skinSelector);
        }
    }

    public void Die() {
        gm.IncramentDeath();
        audioController.PlayDieSound();
        SetMaxDistance();
        transform.position = gm.GetRespawnPoint() + spawnOffset;
        rb.velocity = Vector2.zero;
        camera.SnapCamera();
    }
}
