using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{

    private Sprite skin;

    public Sprite Cloud;
    public Sprite Squish;
    public Sprite Jump;

    void Start () {
    }

    public void SetBaseSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Cloud;
    }

    public void SetSquishSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Squish;
    }

    public void SetJumpSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Jump;
    }
}
