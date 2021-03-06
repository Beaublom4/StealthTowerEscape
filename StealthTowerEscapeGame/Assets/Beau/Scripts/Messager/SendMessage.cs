using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    public string message;

    public Messager messagerScript;
    public PopUps popUpScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            messagerScript.AddMessage(message);
            popUpScript.Message();
            Destroy(gameObject);
        }
    }
}