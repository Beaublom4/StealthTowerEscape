using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float range, damage, backstabDamage, cooldown;
    public Transform crTrans;

    bool canHit = true;
    public Animator anim;

    public Transform hitKnifePos;
    public GameObject HitParticles;

    void Update()
    {
        if (canHit && Input.GetButtonDown("Fire1"))
        {
            canHit = false;
            Invoke("HitCooldown", cooldown);
            anim.SetTrigger("Hit");
        }
    }
    public void DoHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(crTrans.position, transform.forward, out hit, range))
        {
            print(hit.collider.gameObject.name);
            if (hit.transform.tag == "Backstab")
            {
                print("backstab");
                hit.transform.gameObject.GetComponentInParent<EnemyHealth>().health -= backstabDamage;
                SpawnParticles();
            }
            else if (hit.transform.tag == "Enemy")
            {
                print("normal");
                hit.transform.gameObject.GetComponent<EnemyHealth>().health -= damage;
                SpawnParticles();
            }
        }
    }
    void HitCooldown()
    {
        canHit = true;
    }
    void SpawnParticles()
    {
        Destroy(Instantiate(HitParticles, hitKnifePos.position, Quaternion.identity, null), 3);
    }
}
