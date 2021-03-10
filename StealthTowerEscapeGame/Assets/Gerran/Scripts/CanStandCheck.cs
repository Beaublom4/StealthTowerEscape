using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStandCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            GetComponentInParent<PlayerMovement>().canStandup = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
        {
            GetComponentInParent<PlayerMovement>().canStandup = true;
        }
    }
}
