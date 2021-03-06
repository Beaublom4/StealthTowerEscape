using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
	public GameObject message;
	public void Message() 
	{
		message.transform.SetAsFirstSibling();
		message.gameObject.SetActive(true);
	}
	public void OpenedMessager()
    {
		message.gameObject.SetActive(false);
    }
}