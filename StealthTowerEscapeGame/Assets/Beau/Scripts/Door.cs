using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public Animator anim;

    public AudioSource sound;
    public AudioClip openSound, closeSound;
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
    public void StartOpenCoroutine()
    {
        StartCoroutine(CoroutineOpen());
    }
    IEnumerator CoroutineOpen()
    {
        open = true;
        anim.SetBool("Open", true);
        yield return new WaitForSeconds(2);
        anim.SetBool("Open", false);
        open = false;
    }
    public void DoorOpenSound()
    {
        sound.clip = openSound;
        sound.Play();
    }
    public void DoorCloseSound()
    {
        sound.clip = closeSound;
        sound.Play();
    }
}