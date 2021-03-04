using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShoot : MonoBehaviour
{
    [SerializeField] GuardMove moveScript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            moveScript.agent.speed = 0;
            print("Shooting");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            moveScript.agent.speed = moveScript.walkingSpeed;
            print("Stop shooting");
        }
    }
}