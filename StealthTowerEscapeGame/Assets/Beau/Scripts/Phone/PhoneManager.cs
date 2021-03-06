using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] TMP_Text timeText;
    [SerializeField] AudioSource hover, press;
    private void Update()
    {
        double t = (double)Time.timeSinceLevelLoad;
        TimeSpan time = TimeSpan.FromSeconds(t);
        string displayTime = string.Format("{0:D2}:{1:D2}", time.Hours, time.Minutes);

        timeText.text = displayTime;
    }
    public void PressHome()
    {
        SelectApp("home");
        Press();
    }
    public void PressBack()
    {
        print("Test");
        Press();
    }
    public void ButtonMap()
    {
        SelectApp("map");
        Press();
    }
    public void ButtonSkillTree()
    {
        SelectApp("skillTree");
        Press();
    }
    public void ButtonMessager()
    {
        SelectApp("messager");
        Press();
    }
    public void ButtonSounds()
    {
        SelectApp("sounds");
        Press();
    }
    public void ButtonSettings()
    {
        SelectApp("settings");
        Press();
    }

    void SelectApp(string name)
    {
        print("select: " + name);
        foreach (GameObject g in panels)
        {
            if (g.GetComponent<PhoneApp>().appName != name)
                g.SetActive(false);
            else
                g.SetActive(true);
        }
    }
    public void Press()
    {
        press.Play();
    }
    public void Hover()
    {
        hover.Play();
    }
}