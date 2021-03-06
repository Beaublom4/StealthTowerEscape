using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] TMP_Text timeText;
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
    }
    public void PressBack()
    {
        print("Test");
    }
    public void ButtonMap()
    {
        SelectApp("map");
    }
    public void ButtonSkillTree()
    {
        SelectApp("skillTree");
    }
    public void ButtonMessager()
    {
        SelectApp("messager");
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
}