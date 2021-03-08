using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShoot : MonoBehaviour
{
    [SerializeField] GuardMove moveScript;
    [SerializeField] bool shooting;
    [SerializeField] GunSOB gun;
    [SerializeField] LayerMask guardMask, shootMask;
    [SerializeField] GameObject shotTrail;
    [SerializeField] AudioSource gunShot;

    [SerializeField] Transform gunSpot;

    GameObject player;
    SphereCollider triggerCol;
    float timer;
    private void Awake()
    {
        triggerCol = GetComponent<SphereCollider>();
        triggerCol.radius = gun.range;
        gunShot.clip = gun.shotSound;
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
                return;
            if(guard != moveScript.gameObject.GetComponent<Collider>())
                guard.GetComponent<GuardMove>().AttackPlayer(player);
        }

        GameObject bullet = Instantiate(shotTrail, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
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
            print("Shooting");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            shooting = false;
            moveScript.agent.speed = moveScript.walkingSpeed;
            player = null;
            print("Stop shooting");
        }
    }
}