using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    GameObject[] platformGameObjects;
    // LinkedList<GameObject> platforms;

    int currentCheckPoint = 0;

    void Start() {
        platformGameObjects = GameObject.FindGameObjectsWithTag("Platform");
        Array.Sort(platformGameObjects, (a,b) => a.name.CompareTo(b.name));
        // Array.Reverse(platformGameObjects);
        // platforms = new LinkedList<GameObject>(platformGameObjects);
    }

    void Update() {

    }

    void FindCurrentCheckPoint(CheckPoint checkPoint) 
    {
        while (platformGameObjects[currentCheckPoint].GetComponent<CheckPoint>() != checkPoint) {
            if (currentCheckPoint < platformGameObjects.Length) {
                currentCheckPoint++;
            }
        }
    }
}



// public GameObject[] Gates;
 
//     void Start()
//     {
//         Gates = GameObject.FindGameObjectsWithTag("gate");
//         Array.Sort(Gates, (a,b) => a.name.CompareTo(b.name));
//     }