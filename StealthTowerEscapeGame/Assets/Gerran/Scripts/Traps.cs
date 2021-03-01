using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StuntTraps : MonoBehaviour
{

    public float range;
    public Transform Trans;
    public bool isStuned;
    public float timer, stunTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Trans.position, transform.forward * range, Color.green, 1, true);
        RaycastHit hit;
        if(Physics.Raycast(Trans.position, transform.forward, out hit, range))
        {
            
            if (hit.transform.tag == "Enemy" && isStuned == false)
            {
                //hit.transform.gameObject.GetComponents<NavMeshAgent>().Speed = 0;
                isStuned = true;
            }
        }

        if(isStuned == true && timer <= stunTime)
        {
            timer += Time.deltaTime;
        }

        else if(isStuned == true && timer >= stunTime)
        {
            isStuned = false;
            timer = 0;
        }
    }
}
