using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShoot : MonoBehaviour
{
    [SerializeField] GuardMove moveScript;
    [SerializeField] bool shooting;
    [SerializeField] GunSOB gun;

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
        player.GetComponent<Health>().GetDamage(gun.damage);
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