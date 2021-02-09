using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetSpotted : MonoBehaviour
{
	public float spottedMeter;
    [SerializeField] float decreasingSpeed; //In seconds

    GameObject guard;
    float increaseSpeed;
    bool increase;

    bool isAttacked;
    private void Update()
    {
        if(increase == true)
        {
            spottedMeter += increaseSpeed * Time.deltaTime;
            spottedMeter = Mathf.Clamp(spottedMeter, 0, 100);
            if(spottedMeter >= 100 && isAttacked == false)
            {
                isAttacked = true;
                guard.GetComponentInChildren<GuardMove>().AttackPlayer(gameObject);
            }
        }
        else if(increase == false && spottedMeter > 0)
        {
            spottedMeter -= decreasingSpeed * Time.deltaTime;
        }
    }
    public void IncreaseSpottedMeter(float _increaseSpeed, GameObject _guard)
    {
        guard = _guard;
        increaseSpeed = _increaseSpeed;
        increase = true;
    }
    public void DecreaseSpottedMeter()
    {
        increase = false;
        increaseSpeed = 0;
    }
}