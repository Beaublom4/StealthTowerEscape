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

    [SerializeField] Transform gunSpot;

    GameObject player;
    SphereCollider triggerCol;
    float timer;
    private void Awake()
    {
        triggerCol = GetComponent<SphereCollider>();
        triggerCol.radius = gun.range;
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
        Collider[] guards = Physics.OverlapSphere(transform.position, 15, guardMask);
        print(guards.Length);
        foreach(Collider guard in guards)
        {
            guard.GetComponent<GuardMove>().AttackPlayer(player);
        }

        GameObject trail = Instantiate(shotTrail, transform.position, Quaternion.LookRotation(player.transform.position - transform.position));
        trail.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1500);
        Destroy(trail, 3);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position, out hit, 100, shootMask, QueryTriggerInteraction.Ignore))
        {
            if(hit.collider.tag == "Player")
            {
                player.GetComponent<Health>().GetDamage(gun.damage);
            }
        }
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