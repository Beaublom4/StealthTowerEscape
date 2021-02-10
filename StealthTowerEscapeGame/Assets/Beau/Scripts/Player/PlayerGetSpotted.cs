using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetSpotted : MonoBehaviour
{
	public float spottedMeter;
    [SerializeField] float decreasingSpeed; //In seconds

    GameObject spotObj;
    float increaseSpeed;
    bool increase, isSpotted;
    private void Update()
    {
        if(increase == true)
        {
            spottedMeter += increaseSpeed * Time.deltaTime;
            spottedMeter = Mathf.Clamp(spottedMeter, 0, 100);
            if (spottedMeter >= 100 && isSpotted == false)
            {
                isSpotted = true;
                spotObj.GetComponentInChildren<GuardMove>()?.AttackPlayer(gameObject);
                spotObj.GetComponentInChildren<CameraVision>()?.AttackPlayer();
            }
        }
        else if(increase == false && spottedMeter > 0)
        {
            isSpotted = false;
            spottedMeter -= decreasingSpeed * Time.deltaTime;
            spotObj = null;
            spottedMeter = Mathf.Clamp(spottedMeter, 0, 100);
        }
    }
    public void IncreaseSpottedMeter(float _increaseSpeed, GameObject _spotObj)
    {
        spotObj = _spotObj;
        increaseSpeed = _increaseSpeed;
        increase = true;
    }
    public void DecreaseSpottedMeter()
    {
        increase = false;
        increaseSpeed = 0;
    }
}