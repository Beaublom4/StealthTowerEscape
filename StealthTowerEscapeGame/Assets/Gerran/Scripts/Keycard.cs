using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{

    public float range;
    public Transform camTrans;

    public bool hasKeycard;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(camTrans.position, transform.forward, out hit, range))
        {
            if(Input.GetButton("Fire1") && hit.transform.tag == "Keycard" && hasKeycard == false)
            {
                hasKeycard = true;
                Destroy(hit.transform.gameObject);
            }

            if(hit.transform.tag == "Scanner" && Input.GetButton("Fire1") && hasKeycard == true)
            {
                hasKeycard = false;
                Destroy(door);
            }
        }
    }
}
