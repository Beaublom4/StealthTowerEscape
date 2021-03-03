using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (open)
            anim.SetBool("Open", true);
    }
    public void Open_Close()
    {
        print(!open);
        if (open)
        {
            open = false;
            anim.SetBool("Open", false);
        }
        else
        {
            open = true;
            anim.SetBool("Open", true);
        }
    }
}