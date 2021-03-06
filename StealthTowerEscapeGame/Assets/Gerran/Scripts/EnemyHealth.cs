using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void GetDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }
        GetComponentInParent<GuardMove>().AttackPlayer(player);
    }
}
