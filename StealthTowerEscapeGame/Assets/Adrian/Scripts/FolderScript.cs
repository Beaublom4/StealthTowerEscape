using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderScript : MonoBehaviour
{
    [Header("Animator")]
    public Animator folderAnimator;

    [Header("Objects")]
    public GameObject folderMenu;

    [Header("Variables")]
    public float openTime = 1f;



    void Start()
    {
        folderAnimator.SetBool("CanOpen", false);
    }


    void Update()
    {
        
    }

    public void OpenFolder()
    {
        folderAnimator.SetBool("CanOpen", true);
    }

    public void CloseFolder()
    {
        folderAnimator.SetBool("CanOpen", false);
    }
}
