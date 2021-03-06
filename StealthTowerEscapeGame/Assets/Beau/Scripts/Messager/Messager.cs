using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Messager : MonoBehaviour
{
    public GameObject messageItem;
    public PopUps popUpScript;

    public Transform contantParent;
    private void OnEnable()
    {
        popUpScript.OpenedMessager();
    }
    public void AddMessage(string msg)
    {
        GameObject msgItem = Instantiate(messageItem, contantParent);
        msgItem.GetComponentInChildren<TMP_Text>().text = msg;
    }
}