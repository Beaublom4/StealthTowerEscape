using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShoot : MonoBehaviour
{
    [SerializeField] GuardMove moveScript;
    [SerializeField] bool shooting;

    [SerializeField] Animator anim;
    [SerializeField] GunSOB gun;
    [SerializeField] GameObject[] guns;
    [SerializeField] Transform shootPos;

    [SerializeField] LayerMask guardMask, shootMask;
    [SerializeField] GameObject shotTrail;
    [SerializeField] AudioSource gunShot;

    GameObject player;
    SphereCollider triggerCol;
    float timer;
    private void Awake()
    {
        triggerCol = GetComponent<SphereCollider>();
        triggerCol.radius = gun.range;
        gunShot.clip = gun.shotSound;

        foreach(GameObject g in guns)
        {
            if(g.GetComponent<Gun>().name == gun.name)
            {
                g.SetActive(true);
                anim.SetBool(gun.name, true);
                shootPos = g.GetComponent<Gun>().shootPos;
            }
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (shooting)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = gun.fireRate;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        triggerCol.radius += 0.1f;
        gunShot.Play();
        Collider[] guards = Physics.OverlapSphere(transform.position, 15, guardMask);
        foreach(Collider guard in guards)
        {
            if (guard.tag != "Enemy")
                continue;
            if(guard != moveScript.gameObject.GetComponent<Collider>())
                guard.GetComponent<GuardMove>().AttackPlayer(player);
        }

        GameObject bullet = Instantiate(shotTrail, shootPos.position, Quaternion.LookRotation(player.transform.position - shootPos.position),null);
        bullet.GetComponent<Bullet>().dmg = gun.damage;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1500);
        triggerCol.radius -= 0.1f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            moveScript.agent.speed = 0;
            player = other.gameObject;
            shooting = true;
            anim.SetBool("Walking", false);
            print("Shooting");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            StopShooting();
        }
    }
    public void StopShooting()
    {
        shooting = false;
        moveScript.agent.speed = moveScript.walkingSpeed;
        player = null;
        print("Stop shooting");
        anim.SetBool("Walking", true);
    }
}