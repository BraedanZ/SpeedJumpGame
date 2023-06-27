using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public GameObject cam;

    private GameMaster gm;
    private Player player;

    private Vector3 startPosition;

    private Vector3 lerpedPosition;
    private float lerpedX;
    private float lerpedY;

    public float parabolaAmplifier;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        startPosition = new Vector3(-11.6f, 2.2f, 0f);
    }

    void Update()
    {
        lerpedX = cam.transform.position.x - 25.6f + (51 * (player.playerPosition.x / gm.mapLength));
        lerpedY = ((parabolaAmplifier) * Mathf.Pow((lerpedX - (gm.mapLength / 2f)), 2f)) + 13f;
        lerpedPosition.x = lerpedX;
        lerpedPosition.y = lerpedY + cam.transform.position.y;
        lerpedPosition.z = 0f;
        transform.position = Vector3.MoveTowards(transform.position, lerpedPosition, 2f * Time.deltaTime);
    }
}



