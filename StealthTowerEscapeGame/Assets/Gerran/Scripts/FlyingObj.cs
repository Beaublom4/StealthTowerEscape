using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObj : MonoBehaviour
{
    public float range, velosity, speed;
    public Transform crTrans;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(crTrans.position, transform.forward , out hit, range))
        {
            if(hit.transform.tag == "Object" && Input.GetButton("Fire1"))
            {
                if (hit.transform.GetComponent<Interect>().hasPickedUp == true)
                {
                    print("throw test");
                    hit.transform.GetComponent<Rigidbody>().velocity = transform.forward * (velosity * speed) * Time.deltaTime;
                    hit.transform.parent = null;
                    hit.transform.GetComponent<Rigidbody>().useGravity = true;
                    hit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    hit.transform.GetComponent<Interect>().hasPickedUp = false;
                }

                else
                {
                    hit.rigidbody.velocity = transform.forward * (velosity * speed) * Time.deltaTime;
                }
            }
        }
    }
}
