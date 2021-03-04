using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhone : MonoBehaviour
{
    public GameObject phone;
    public MouseLook cameraScript;
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if (!phone.activeSelf)
            {
                phone.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraScript.enabled = false;
            }
            else
            {
                phone.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraScript.enabled = true;
            }
        }
    }
}