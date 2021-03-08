using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool canPlay = true;
    AudioSource hit;
    private void Awake()
    {
        hit = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!canPlay || collision.collider.CompareTag("Player"))
            return;
        hit.pitch = Random.Range(.5f, 1.5f);
        hit.Play();
    }
}