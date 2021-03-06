﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObj : MonoBehaviour
{
    public float range, velosity, speed;
    public Transform crTrans;

    public Transform pickupTrans, level1Start, player;
    public GameObject dest;

    public bool hasPickedUp;

    public SkinnedMeshRenderer knife;
    public Animator anim;

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(crTrans.position, transform.forward , out hit, range))
        {
            if(hit.transform.tag == "Object")
            {
                print("hit");
                if(Input.GetButton("Fire2") && hasPickedUp == false)
                {
                    print("getem");
                    knife.enabled = false;
                    anim.SetTrigger("PickUp");
                    hasPickedUp = true;
                    hit.collider.GetComponent<Item>().canPlay = false;
                    hit.transform.GetComponent<Rigidbody>().useGravity = true;
                    hit.transform.position = pickupTrans.position;
                    hit.transform.parent = dest.transform;
                    hit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                }
                if (Input.GetKeyDown("g"))
                {
                    if (hasPickedUp == true)
                    {
                        print("throw test");
                        knife.enabled = true;
                        anim.SetTrigger("Drop");
                        hit.collider.GetComponent<Item>().canPlay = true;
                        hit.transform.parent = null;
                        hit.transform.GetComponent<Rigidbody>().useGravity = true;
                        hit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        hasPickedUp = false;
                        hit.transform.GetComponent<Rigidbody>().velocity = transform.forward * (velosity * speed) * Time.deltaTime;
                    }
                    else
                    {
                        hit.rigidbody.velocity = transform.forward * (velosity * speed) * Time.deltaTime;
                    }
                }
            }
            if (Input.GetKeyDown("e"))
            {
                if (hit.transform.tag == "Door")
                {
                    hit.transform.GetComponent<Door>().Open_Close();

                }

                if(hit.transform.tag == "Lift")
                {
                    print("testlift");
                    GetComponentInParent<CharacterController>().enabled = false;
                    player.position = level1Start.position;
                    GetComponentInParent<CharacterController>().enabled = true;
                }
            }
        }
    }
}
