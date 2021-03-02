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

        else if(Input.GetMouseButtonUp(1) && hasPickedUp == true)
        {
            print("drop test");
            this.transform.parent = null;
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hasPickedUp = false;
        }
    }
}
