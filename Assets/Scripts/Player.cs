using System;
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

    private SunQuotes sunQuotes;

    public CheckpointController checkpointController;

    public PauseMenu pauseMenu;

    public AnimatePlayer animatePlayer;

    private new CameraFollow camera;

    [SerializeField] FlashImage flashImage = null;

    public float verticalSpeed;
    public float horizontalSpeed;

    double pressSpaceTime = 0f;

    bool isGrounded;
    public LayerMask whatIsGround;
    public LayerMask whatIsIce;

    bool spacePressed;

    public Vector2 spawnOffset;

    private AudioController audioController;

    private bool hasLanded;

    public float maxDistance;

    private int skinSelector;

    public float xToMaxXRatio;

    private bool canJump = true;

    // private bool iceZone = false;
    // public float iceStart;
    // public float iceEnd;

    private bool mountainZone = false;
    public float mountainStart;
    public float mountainEnd;

    public float horizontalWindStrength;

    private bool leftWindZone = false;
    public float leftWindStart;
    public float leftWindEnd;

    private bool rightWindZone = false;
    public float rightWindStart;
    public float rightWindEnd;

    public float verticalWindStrength;

    private bool downWindZone = false;
    public float downWindStart;
    public float downWindEnd;

    private bool upWindZone = false;
    public float upWindStart;
    public float upWindEnd;

    public float waterStart;
    public float waterEnd;

    public bool hoveringPause;

    private bool justJumped;
    private float timeInAir;

    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        sunQuotes = GameObject.FindGameObjectWithTag("Sun").GetComponent<SunQuotes>();
        checkpointController = GameObject.FindGameObjectWithTag("CheckpointController").GetComponent<CheckpointController>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        animatePlayer = GameObject.FindGameObjectWithTag("Skin").GetComponent<AnimatePlayer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        // maxDistance = transform.position.x;
        SelectSkin();
        // transform.position = checkpointController.GetRespawnPoint() + spawnOffset;
        // SetLoadedPosition();
        // SetLoadedVelocity();
        camera.SnapCamera();
        pauseMenu.UnpauseGame();
    }

    public void Restart() {
        transform.position = checkpointController.startPosition;
        rb.velocity = Vector2.zero;
        camera.SnapCamera();
        maxDistance = 0;
    }

    void FixedUpdate() 
    {
        LocatePlayer();
        AddJumpTime();
        // StopJump();
        ApplyWind();
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
        CalculateTimeInAir();
        // IceZoneCheck();
        MountainZoneCheck();
        LeftWindZoneCheck();
        RightWindZoneCheck();
        DownWindZoneCheck();
        UpWindZoneCheck();
        WaterZoneCheck();
        
    }

    private void SetMaxDistance() {
        if (transform.position.x > maxDistance) {
            maxDistance = transform.position.x;
            if (maxDistance < gm.mapLength / 3) {
                sunQuotes.SetHappiness(1);
            } else if (maxDistance >= gm.mapLength * 0.33f && maxDistance < gm.mapLength * 0.66f) {
                sunQuotes.SetHappiness(2);
            } else if (maxDistance >= gm.mapLength * 0.66f) {
                sunQuotes.SetHappiness(3);
            }
        }
        
    }

    private void DetectSpaceInput() 
    {
        if (!pauseMenu.isPaused) {
            if (Input.GetKeyDown("space")) {
                spacePressed = true;
                audioController.PlayJumpStartSound();
                canJump = true;
            } else if (!hoveringPause) {
                if (Input.GetKeyDown(KeyCode.Mouse0)) {
                    spacePressed = true;
                    audioController.PlayJumpStartSound();
                    canJump = true; 
                }
            }

            if (Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.Mouse0)) {
                spacePressed = false;
                Jump();
            }
        }
    }

    private void Pause() {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape")) {
            if (pauseMenu.isPaused) {
                pauseMenu.UnpauseGame();
            } else {
                pauseMenu.PauseGame();
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

    public void SelectingPauseButton() {
        hoveringPause = true;
    }

    public void UnSelectingPauseButton() {
        hoveringPause = false;
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

        if (pressSpaceTime < 0.16f) {
            pressSpaceTime = 0.16f;
        }

        double temp = Math.Floor(pressSpaceTime * 100);
        if (temp % 2 == 1) {
            temp -= 1;
        }
        pressSpaceTime = temp / 100;
    }

    private void GroundCheck() {
        if (Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, -transform.up, 0.1f, whatIsGround)) {
            if (!isGrounded && !hasLanded) {
                audioController.PlayLandSound();
                hasLanded = true;
            }
            isGrounded = true;
            StopJump();
            SelectSkin();
        } else if (Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, -transform.up, 0.1f, whatIsIce)) { 
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

    private void CalculateTimeInAir() {
        if (justJumped) {
            timeInAir += Time.deltaTime;
        }
        if (timeInAir > 0.1f) {
            justJumped = false;
            timeInAir = 0f;
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
            if (!mountainZone) {
                justJumped = true;
                rb.AddForce(transform.right * horizontalSpeed * (float)pressSpaceTime);
                rb.AddForce(transform.up * verticalSpeed * (float)pressSpaceTime);
            } else if (mountainZone) {
                justJumped = true;
                rb.AddForce(transform.right * (horizontalSpeed - 400) * (float)pressSpaceTime);
                rb.AddForce(transform.up * (verticalSpeed + 400) * (float)pressSpaceTime);
            }
            canJump = false;
            audioController.PlayJumpEndSound();
            hasLanded = false;
            gm.IncramentJumps();
        }
    }

    private void StopJump() {
        if (isGrounded && !justJumped) {
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
        flashImage.StartFlash((0.2f + 0.15f * checkpointController.fallsInARow), 0.1f + 0.15f * checkpointController.fallsInARow, Color.white);
        gm.IncramentDeath();
        audioController.PlayDieSound();
        SetMaxDistance();
        transform.position = checkpointController.GetRespawnPoint() + spawnOffset;
        rb.velocity = Vector2.zero;
        camera.SnapCamera();
        sunQuotes.Died();
        // gm.SavePlayer();
    }

    private void ApplyWind() {
        if (leftWindZone) {
            rb.AddForce(-transform.right * horizontalWindStrength);
        } else if (rightWindZone) {
            rb.AddForce(transform.right * horizontalWindStrength);
        } else if (downWindZone) {
            rb.AddForce(-transform.up * verticalWindStrength);
        } else if (upWindZone) {
            rb.AddForce(transform.up * verticalWindStrength);
        }
    }

    // private void IceZoneCheck() {
    //     if (playerPosition.x > iceStart && playerPosition.x < iceEnd) {
    //         iceZone = true;
    //     } else {
    //         iceZone = false;
    //     }
    // }

    private void MountainZoneCheck() {
        if (playerPosition.x > mountainStart && playerPosition.x < mountainEnd) {
            mountainZone = true;
        } else {
            mountainZone = false;
        }
    }

    private void LeftWindZoneCheck() {
        if (playerPosition.x > leftWindStart && playerPosition.x < leftWindEnd) {
            leftWindZone = true;
        } else {
            leftWindZone = false;
        }
    }

    private void RightWindZoneCheck() {
        if (playerPosition.x > rightWindStart && playerPosition.x < rightWindEnd) {
            rightWindZone = true;
        } else {
            rightWindZone = false;
        }
    }

    private void DownWindZoneCheck() {
        if (playerPosition.x > downWindStart && playerPosition.x < downWindEnd) {
            downWindZone = true;
        } else {
            downWindZone = false;
        }
    }

    private void UpWindZoneCheck() {
        if (playerPosition.x > upWindStart && playerPosition.x < upWindEnd) {
            upWindZone = true;
        } else {
            upWindZone = false;
        }
    }

    private void WaterZoneCheck() {
        if (playerPosition.x > waterStart && playerPosition.x < waterEnd) {
            if (!gm.IsWaterZone()) {
                gm.ActivateWaterZone();
            }
        } else {
            if (gm.IsWaterZone()) {
                gm.DeactivateWaterZone();
            }
        }
    }

    // public void SetPosition(Vector3 position) {
    //     transform.position = position;
    // }

    public Vector3 GetPosition() {
        return transform.position;
    }

    // public void SetVelocity(Vector2 velocity) {
    //     rb.velocity = velocity;
    // }

    public Vector3 GetVelocity() {
        return rb.velocity;
    }

    // private void SetLoadedPosition() {
    //     transform.position = gm.GetLoadedPosition();
    // }

    // private void SetLoadedVelocity() {
    //     rb.velocity = gm.GetLoadedVelocity();
    // }
}
