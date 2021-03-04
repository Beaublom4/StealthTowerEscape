using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject player;

    public Transform[] spawnPoints;
    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            print("Loaded Tutorial Level");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoints[0].position;
            player.GetComponent<CharacterController>().enabled = true;
        }
        else if (Input.GetKeyDown("2"))
        {
            print("Loaded Level 1");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoints[1].position;
            player.GetComponent<CharacterController>().enabled = true;
        }
        else if (Input.GetKeyDown("3"))
        {
            print("Loaded Level 2");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoints[2].position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}