using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhone : MonoBehaviour
{
    public GameObject phone, phoneIcon;
    public MouseLook cameraScript;
    public Melee meleeScript;
    private void Update()
    {

        if(Input.GetButtonDown("Cancel"))
        {
            if (!phone.activeSelf)
            {
                if (phoneIcon.activeSelf)
                    phoneIcon.SetActive(false);

                phone.SetActive(true);
                meleeScript.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraScript.enabled = false;
            }
            else
            {
                phone.SetActive(false);
                meleeScript.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraScript.enabled = true;
            }
        }
    }
}