using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
	public GameObject message;
	public AudioSource messageSound, vibrateSound;
	public GameObject phoneIcon;
	public void Message() 
	{
		messageSound.Play();
		vibrateSound.Play();
		message.transform.SetAsFirstSibling();
		message.gameObject.SetActive(true);
		phoneIcon.SetActive(true);
		phoneIcon.GetComponent<Animator>().SetTrigger("Shake");
	}
	public void OpenedMessager()
    {
		message.gameObject.SetActive(false);
    }
}