using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float range, damage, backstabDamage, cooldown;
    public Transform crTrans;
    public LayerMask mask;
    public AudioSource knifeSource;
    public AudioClip knifeWall, knifeEnemy;

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
        if (Physics.Raycast(crTrans.position, transform.forward, out hit, range, mask, QueryTriggerInteraction.Ignore))
        {
            print(hit.collider.gameObject.name);
            if (hit.transform.tag == "Backstab")
            {
                print("backstab");
                knifeSource.clip = knifeEnemy;
                knifeSource.volume = .7f;
                knifeSource.Play();
                hit.transform.gameObject.GetComponentInParent<EnemyHealth>().GetDamage(backstabDamage);
                SpawnParticles();
            }
            else if (hit.transform.tag == "Enemy")
            {
                print("normal");
                knifeSource.clip = knifeEnemy;
                knifeSource.volume = .7f;
                knifeSource.Play();
                hit.transform.gameObject.GetComponent<EnemyHealth>().GetDamage(damage);
                SpawnParticles();
            }
            else
            {
                knifeSource.clip = knifeWall;
                knifeSource.volume = 1;
                knifeSource.Play();
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
