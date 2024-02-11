using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource jumpStartSound;
    private AudioSource jumpEndSound;
    private AudioSource dieSound;
    private AudioSource land;
    private AudioSource ding;

    void Start()
    {
        jumpStartSound = GameObject.FindGameObjectWithTag("JumpStartSound").GetComponent<AudioSource>();
        jumpEndSound = GameObject.FindGameObjectWithTag("JumpEndSound").GetComponent<AudioSource>();
        dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();
        land = GameObject.FindGameObjectWithTag("Land").GetComponent<AudioSource>();
        ding = GameObject.FindGameObjectWithTag("DingSound").GetComponent<AudioSource>();
    }

    public void PlayJumpStartSound() {
        jumpStartSound.Play();
    }

    public void PlayJumpEndSound() {
        jumpEndSound.Play();
    }

    public void PlayDieSound() {
        dieSound.Play();
    }

    public void PlayLandSound() {
        land.Play();
    }

    public void PlayDingSound() {
        ding.Play();
    }
}
