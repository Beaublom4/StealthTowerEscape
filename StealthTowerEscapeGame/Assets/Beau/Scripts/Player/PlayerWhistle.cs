using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWhistle : MonoBehaviour
{
    public float searchTime;

    public float range;
    public LayerMask mask;
    private void Update()
    {
        if (Input.GetButtonDown("Whistle"))
        {
            print("Whistle");
            Collider[] guards = Physics.OverlapSphere(transform.position, range, mask);
            foreach(Collider col in guards)
            {
                col.GetComponent<GuardMove>().Whistle(transform.position, searchTime);
            }
        }
    }
}