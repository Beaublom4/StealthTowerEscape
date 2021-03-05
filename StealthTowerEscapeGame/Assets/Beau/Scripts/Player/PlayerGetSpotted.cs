using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGetSpotted : MonoBehaviour
{
	public float spottedMeter;
    [SerializeField] float decreasingSpeed; //In seconds

    [SerializeField] Slider spotSlider;
    bool transition;

    GameObject spotObj;
    float increaseSpeed;
    [HideInInspector] public bool increase, isSpotted;
    private void Update()
    {
        if(increase == true && !isSpotted)
        {
            spottedMeter += increaseSpeed * Time.deltaTime;
            spottedMeter = Mathf.Clamp(spottedMeter, 0, 100);
            spotSlider.gameObject.SetActive(true);
            spotSlider.value = spottedMeter;
            if (spottedMeter >= 100 && isSpotted == false)
            {
                isSpotted = true;
                spotSlider.gameObject.SetActive(false);
            }
        }
        else if(increase == false && spottedMeter > 0)
        {
            isSpotted = false;
            spottedMeter -= decreasingSpeed * Time.deltaTime;
            spotObj = null;
            spottedMeter = Mathf.Clamp(spottedMeter, 0, 100);
            spotSlider.value = spottedMeter;
        }
        else if(spotSlider.gameObject.activeSelf && spottedMeter <= 0)
        {
            spotSlider.gameObject.SetActive(false);
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