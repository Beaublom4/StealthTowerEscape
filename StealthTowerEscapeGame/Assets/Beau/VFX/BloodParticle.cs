using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    public GameObject bloodSplat;
    public ParticleSystem partSyst;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public float sizeMin, sizeMax;
    private void OnParticleCollision(GameObject other)
    {
        int numCollissionEvents = partSyst.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while(i < numCollissionEvents)
        {
            GameObject g = Instantiate(bloodSplat, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].intersection), null);
            float randomSize = UnityEngine.Random.Range(sizeMin, sizeMax);
            Math.Round(randomSize, 1);
            g.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            i++;
        }
    }
}