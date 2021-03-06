using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{

    public float health;
    float maxHealth;
    public Vector3 reSpawnPoint;
    public TMP_Text text;

    private void Awake()
    {
        maxHealth = health;
        HealthDisplay();
    }
    public void GetDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            print("Dood");
            GetComponent<CharacterController>().enabled = false;
            this.transform.position = reSpawnPoint;
            GetComponent<CharacterController>().enabled = true;
            health = maxHealth;
        }
        HealthDisplay();
    }
    public void GetHealth(float _health)
    {
        health += _health;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        HealthDisplay();
    }
    public void HealthDisplay()
    {
        text.text = health.ToString();
    }
}
