using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        print("Hello");
        if (other.CompareTag("Player")) {
            print("hi");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
