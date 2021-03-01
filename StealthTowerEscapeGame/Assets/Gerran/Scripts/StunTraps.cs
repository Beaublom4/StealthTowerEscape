using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTraps : MonoBehaviour
{

    public float range;
    public Transform Trans;
    public bool soundPlay;

    public AudioSource alarm;
    public AudioClip alarmSounds;

    // Start is called before the first frame update
    void Start()
    {
        soundPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Trans.position, transform.forward * range, Color.yellow, 1, true);
        RaycastHit hit;
        if(Physics.Raycast(Trans.position, transform.forward, out hit, range))
        {
            
            if (hit.transform.tag == "Enemy" && soundPlay == false)
            {
                alarm.PlayOneShot(alarmSounds);
                soundPlay = true;
            }
        }
    }
}
