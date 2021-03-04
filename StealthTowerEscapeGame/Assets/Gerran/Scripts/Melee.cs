using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public float range, damage, backstabDamage;
    public Transform crTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            RaycastHit hit;
            if(Physics.Raycast(crTrans.position, transform.forward, out hit, range))
            {
                print("hit");
                
                if (hit.transform.tag == "Backstab")
                {
                    print("backstab");
                    hit.transform.gameObject.GetComponentInParent<EnemyHealth>().health -= backstabDamage;
                }

                else if (hit.transform.tag == "Enemy")
                {
                    print("normal");
                    hit.transform.gameObject.GetComponent<EnemyHealth>().health -= damage;
                }

                
            }
        }
    }
}
