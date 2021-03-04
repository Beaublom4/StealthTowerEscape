using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float health, maxHealth;
    public Vector3 reSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("i"))
        {
            health -= 50;
        }

        if (health <= 0.000001)
        {
            GetComponent<CharacterController>().enabled = false;
            this.transform.position = reSpawnPoint;
            GetComponent<CharacterController>().enabled = true;
            health = maxHealth;
        }

        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }


}
