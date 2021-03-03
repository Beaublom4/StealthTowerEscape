using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Open_Close()
    {
        if (open)
        {

        }
        else
        {

        }
    }
}