using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public float range, damage, backstabDamage;
    public Transform crTrans;

    public GameObject HitParticles;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            RaycastHit hit;
            if(Physics.Raycast(crTrans.position, transform.forward, out hit, range))
            {
                print(hit.collider.gameObject.name);
                if (hit.transform.tag == "Backstab")
                {
                    print("backstab");
                    hit.transform.gameObject.GetComponentInParent<EnemyHealth>().health -= backstabDamage;
                    Destroy(Instantiate(HitParticles, hit.point, Quaternion.identity, null), 3);
                }
                else if (hit.transform.tag == "Enemy")
                {
                    print("normal");
                    hit.transform.gameObject.GetComponent<EnemyHealth>().health -= damage;
                    Destroy(Instantiate(HitParticles, hit.point, Quaternion.identity, null), 3);
                }

                
            }
        }
    }
}
