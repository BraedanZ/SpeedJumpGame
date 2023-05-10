using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{

    private Sprite skin;

    public Sprite CloudBase;
    public Sprite CloudSquish;
    public Sprite CloudJump;

    void Start () {
    }

    public void SetBaseSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudBase;
    }

    public void SetSquishSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudSquish;
    }

    public void SetJumpSprite() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudJump;
    }
}
