using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using System;

public class Settings : MonoBehaviour
{
	public VolumeProfile profile;
    public float mbNum;
    public TMP_Text motionBlurText;
    public float minMotionBlur, maxMotionBlur;

    private void Awake()
    {
        mbNum = GetMotionBlur().intensity.value;
    }
    private void OnEnable()
    {
        SetMotionBlurText();
    }
    public void ChangeMotionBlur(float value)
    {
        
    }
    MotionBlur GetMotionBlur()
    {
        MotionBlur mb;
        profile.TryGet<MotionBlur>(out mb);
        return mb;
    }
    void SetMotionBlurText()
    {
        if (GetMotionBlur().intensity.value > 0)
        {
            motionBlurText.text = GetMotionBlur().intensity.value.ToString();
        }
        else
        {
            motionBlurText.text = "OFF";
        }
    }
}