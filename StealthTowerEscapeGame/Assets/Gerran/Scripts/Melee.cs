﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Transform cam;

    public float range, damage, backstabDamage, cooldown;
    public Transform crTrans;
    public LayerMask mask;
    public AudioSource knifeSource;
    public AudioClip knifeWall, knifeEnemy;

    public GameObject hitParticle;
    public Color color;

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
        if (Physics.Raycast(crTrans.position, cam.forward, out hit, range, mask, QueryTriggerInteraction.Ignore))
        {
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
                GameObject g = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal), null);
                ParticleSystem.MainModule main = g.GetComponent<ParticleSystem>().main;
                main.startColor = GetHitColor(hit);
                Destroy(g, 3);

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

    Color GetHitColor(RaycastHit hit)
    {
        if (hit.transform.GetComponent<MeshCollider>())
        {
            Renderer renderer = hit.transform.GetComponent<MeshRenderer>();
            Texture2D texture = renderer.material.mainTexture as Texture2D;
            Vector2 pixelUV = hit.textureCoord;
            pixelUV.x *= texture.width;
            pixelUV.y *= texture.height;
            Vector2 tiling = renderer.material.mainTextureScale;
            return texture.GetPixel(Mathf.FloorToInt(pixelUV.x * tiling.x), Mathf.FloorToInt(pixelUV.y * tiling.y));
        }
        else
        {
            return hit.transform.GetComponent<MeshRenderer>().material.color;
        }
    }
}
