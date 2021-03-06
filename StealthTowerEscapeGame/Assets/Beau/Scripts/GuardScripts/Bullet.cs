using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float dmg;
    public LayerMask mask;
    public GameObject blood, dust;

    private void Start()
    {
        Destroy(gameObject, 3);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if (mask == (mask | (1 << other.gameObject.layer)))
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Health>().GetDamage(dmg);
                blood.GetComponent<ParticleSystem>().Play();
                blood.GetComponent<AudioSource>().Play();
                Destroy(gameObject, 1);
            }
        }
    }
}