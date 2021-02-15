using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;

    public void Update()
    {
        if(health <=0.1)
        {
            Destroy(transform.gameObject);
        }
    }
}
