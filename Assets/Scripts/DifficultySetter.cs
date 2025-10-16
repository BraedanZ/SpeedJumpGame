using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    private Player player;
    private GameMaster gm;
    public int difficulty;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            StaticClass.SetDifficulty(difficulty);
            if (difficulty == 4)
            {
                gm.ActivateOverlay();
            }
        }
    }
}
