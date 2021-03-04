using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
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