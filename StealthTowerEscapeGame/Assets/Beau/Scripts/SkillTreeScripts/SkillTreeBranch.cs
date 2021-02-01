using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBranch : MonoBehaviour
{
    public Slider branchSlider;

    public SkillTreeBranch preBranch;
    public bool unlocked;
    [TextArea] public string info;

    public Animator anim;

    [SerializeField] string branchType;
    [SerializeField] int branchValue;

    [SerializeField] SkillTree skillTreeScript;

    public void UnlockBranch()
    {
        unlocked = true;
        print("Unlucked");
    }


    #region Upgrades

    //stealth
    void AddStealth(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    void AddWhistleAbil(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }
    void AddThrowingDistance(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    //camera things
    void DecreaseCamVisibility(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }
    void AddCamDisableAbil(int value)
    {
        
    }
    void AddHeadsetDistortionAbil(int value)
    {

    }

    //damage
    void AddDamage(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    void AddThrowWeaponAbil(int value)
    {

    }
    void AddGrabGuardSpeed(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }

    //speed
    void AddSpeed(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    void AddCrouchWalkSpeed(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }
    void AddSlideAbil(int value)
    {
        switch (value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    #endregion
}