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
                hit.rigidbody.velocity = transform.forward * (velosity * speed) * Time.deltaTime;
            }
        }
    }
}
