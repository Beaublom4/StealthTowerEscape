using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAbils : MonoBehaviour
{
    public TMP_Text abilName, explenation;
    public GameObject hitText;

    public float disableTime;

    public PlayerPhone pPhoneScript;
    public MouseLook camScript;
    public Melee meleeScript;

    string wantedTag;

    public GameObject cam;
    bool shootRay;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            print("cancel");
            shootRay = false;
        }

        if (!shootRay)
            return;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20, -5, QueryTriggerInteraction.Ignore))
        {
            if(hit.collider.tag == wantedTag)
            {
                if (!hitText.activeSelf)
                    hitText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PressE(hit.collider.gameObject);
                }
            }
            else
            {
                if (hitText.activeSelf)
                    hitText.SetActive(false);
            }
        }
    }
    void PressE(GameObject hit)
    {
        if(abilName.text == "Cam Disable")
        {
            hit.GetComponentInParent<CameraVision>().CamDisable(disableTime);
        }
        else if(abilName.text == "")
        {

        }
    }
    public void CamDisable()
    {
        abilName.text = "Cam Disable";
        explenation.text = "Hover over camera to target that camera.";
        wantedTag = "Camera";
        SelectAbil();
    }
    public void HearingDistort()
    {
        abilName.text = "";
        explenation.text = "";
        wantedTag = "Guard";
        SelectAbil();
    }
    public void PhoneAlarm()
    {
        abilName.text = "";
        explenation.text = "";
        wantedTag = "Guard";
        SelectAbil();
    }
    void SelectAbil()
    {
        pPhoneScript.enabled = false;
        camScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        meleeScript.enabled = true;
        shootRay = true;
    }
    public void ShootRayFalse()
    {
        shootRay = false;
    }
}