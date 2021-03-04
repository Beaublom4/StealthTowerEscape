using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FolderScript : MonoBehaviour
{
    [Header("Animator")]
    public Animator folderAnimator;

    [Header("Objects")]
    public GameObject folderMenu;
    public GameObject startButton;
    public GameObject optionsButton;
    public GameObject backButton;
    public GameObject level1Button;

    [Header("Variables")]
    public float openTime = 1f;

    [Header("Scenes")]
    private string naamScene;



    void Start()
    {
        folderAnimator.SetBool("CanOpen", false);

        startButton.SetActive(true);
        optionsButton.SetActive(true);
        backButton.SetActive(false);
        level1Button.SetActive(false);
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

    public void GoToLevels()
    {
        startButton.SetActive(false);
        optionsButton.SetActive(false);
        backButton.SetActive(true);
        level1Button.SetActive(true);
    }

    public void GoBackToMainMenu()
    {
        startButton.SetActive(true);
        optionsButton.SetActive(true);
        backButton.SetActive(false);
        level1Button.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(naamScene);
    }
}
