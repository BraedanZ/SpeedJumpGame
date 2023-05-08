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
        // skin = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetBaseSprite() {
        print("test base");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudBase;
    }

    public void SetSquishSprite() {
        print("test squish");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudSquish;
    }

    public void SetJumpSprite() {
        print("test jump");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CloudJump;
    }
}
