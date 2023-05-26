using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{

    private Sprite skin;

    public Sprite Cloud;
    public Sprite Squish;
    public Sprite Jump;

    public Sprite Angry1;
    public Sprite Angry2;
    public Sprite Angry3;
    public Sprite Angry4;

    public Sprite AngryJump1;
    public Sprite AngryJump2;
    public Sprite AngryJump3;
    public Sprite AngryJump4;

    public Sprite Happy1;
    public Sprite Happy2;
    public Sprite Happy3;
    public Sprite Happy4;

    public Sprite HappyJump1;
    public Sprite HappyJump2;
    public Sprite HappyJump3;
    public Sprite HappyJump4;

    void Start () {
    }
    public void AnimateBase() {
        SetBase();
    }

    public void AnimateSquish() {
        SetSquish();
    }

    public void AnimateJump() {
        SetJump();
    }

    public void SetBase() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Cloud;
    }

    public void SetSquish() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Squish;
    }

    public void SetJump() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Jump;
    }

    public void SetAngry1() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Angry1;
    }

    public void SetAngry2() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Angry2;
    }

    public void SetAngry3() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Angry3;
    }

    public void SetAngry4() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Angry4;
    }

    public void SetAngryJump1() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AngryJump1;
    }

    public void SetAngryJump2() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AngryJump2;
    }

    public void SetAngryJump3() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AngryJump3;
    }

    public void SetAngryJump4() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AngryJump4;
    }

    public void SetHappy1() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Happy1;
    }

    public void SetHappy2() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Happy2;
    }

    public void SetHappy3() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Happy3;
    }

    public void SetHappy4() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Happy4;
    }

    public void SetHappyJump1() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = HappyJump1;
    }

    public void SetHappyJump2() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = HappyJump2;
    }

    public void SetHappyJump3() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = HappyJump3;
    }

    public void SetHappyJump4() {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = HappyJump4;
    }

}
