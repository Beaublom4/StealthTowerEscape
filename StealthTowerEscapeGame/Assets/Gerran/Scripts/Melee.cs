using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Transform cam;

    public float range, damage, backstabDamage, cooldown;
    public Transform crTrans;
    public LayerMask mask;
    public AudioSource knifeSource, fistSource;
    public AudioClip knifeWall, knifeEnemy, punchWall, punchEnemy;

    IEnumerator coroutine;
    public SkinnedMeshRenderer smr;
    public Material blood;
    bool returnBlood;

    public GameObject hitParticle;
    public Color color;

    bool canHit = true;
    public Animator anim;

    public Transform hitKnifePos;
    public GameObject bloodParticles;

    public GameObject knife;

    private void Awake()
    {
        blood = smr.materials[1];
    }
    void Update()
    {
        if (canHit && Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(crTrans.position, cam.forward, out hit, range, mask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.tag == "Backstab")
                {
                    anim.SetBool("BackStab", true);
                }
                else
                {
                    anim.SetBool("BackStab", false);
                }
            }
            else
            {
                anim.SetBool("BackStab", false);
            }
            canHit = false;
            Invoke("HitCooldown", cooldown);
            anim.SetTrigger("Hit");
        }
        if (returnBlood)
        {
            print("test");
            Color c = blood.color;
            c.a -= Time.deltaTime;
            if (c.a < 0)
            {
                returnBlood = false;
                c.a = 0;
            }
            blood.color = c;
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
                PlaySound(true);
                hit.transform.gameObject.GetComponentInParent<EnemyHealth>().GetDamage(backstabDamage);
                BloodOnKnife();
                SpawnParticles(hit.point);
            }
            else if (hit.transform.tag == "Enemy")
            {
                print("normal");
                PlaySound(true);
                hit.transform.gameObject.GetComponent<EnemyHealth>().GetDamage(damage);
                BloodOnKnife();
                SpawnParticles(hit.point);
            }
            else
            {
                GameObject g = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal), null);
                ParticleSystem.MainModule main = g.GetComponent<ParticleSystem>().main;
                main.startColor = GetHitColor(hit);
                Destroy(g, 3);
                PlaySound(false);
            }
        }
    }
    void PlaySound(bool hitEnemy)
    {
        if (hitEnemy)
        {
            if (knife.activeSelf)
            {
                knifeSource.clip = knifeEnemy;
                knifeSource.volume = .7f;
                knifeSource.Play();
            }
            else
            {
                fistSource.clip = punchEnemy;
                fistSource.Play();
            }
        }
        else
        {
            if (knife.activeSelf)
            {
                knifeSource.clip = knifeWall;
                knifeSource.volume = 1;
                knifeSource.Play();
            }
            else
            {
                fistSource.clip = punchWall;
                fistSource.Play();
            }
        }
    }
    void HitCooldown()
    {
        canHit = true;
    }
    void SpawnParticles(Vector3 pos)
    {
        Destroy(Instantiate(bloodParticles, pos, Quaternion.identity, null), 3);
    }
    void BloodOnKnife()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = Blood();
        StartCoroutine(coroutine);
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
    IEnumerator Blood()
    {
        returnBlood = false;
        blood.color = Color.white;
        yield return new WaitForSeconds(5);
        returnBlood = true;
    }
}
