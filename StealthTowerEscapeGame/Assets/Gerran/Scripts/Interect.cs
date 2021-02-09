using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interect : MonoBehaviour
{
    public Transform pickupTrans;
    public GameObject dest;
    public bool hasPickedUp;

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1) && hasPickedUp == false)
        {
            hasPickedUp = true;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = pickupTrans.position;
            this.transform.parent = dest.transform;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        else if(Input.GetMouseButtonUp(1))
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hasPickedUp = false;
        }
    }
}
