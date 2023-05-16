using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource jumpStartSound;
    private AudioSource jumpEndSound;
    private AudioSource dieSound;

    void Start()
    {
        jumpStartSound = GameObject.FindGameObjectWithTag("JumpStartSound").GetComponent<AudioSource>();
        jumpEndSound = GameObject.FindGameObjectWithTag("JumpEndSound").GetComponent<AudioSource>();
        dieSound = GameObject.FindGameObjectWithTag("DieSound").GetComponent<AudioSource>();
    }

    public void PlayJumpStartSound() {
        jumpStartSound.Play();
    }

    public void PlayJumpEndSount() {
        jumpEndSound.Play();
    }

    public void PlayDieSound() {
        dieSound.Play();
    }
}
