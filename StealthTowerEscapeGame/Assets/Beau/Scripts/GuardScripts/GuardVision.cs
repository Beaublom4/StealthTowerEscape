using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    [SerializeField] float fieldOfView;

    GuardMove guardMoveScript;
    bool playerInSight;

    private void Awake()
    {
        guardMoveScript = GetComponentInParent<GuardMove>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            float angle = (Vector3.Angle(direction, Vector3.forward));

            print(angle);
            if (angle < fieldOfView * .5f)
            {
                playerInSight = true;
                guardMoveScript.SetPlayerAsLoc(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}