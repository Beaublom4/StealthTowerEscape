using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderScript : MonoBehaviour
{
    [Header("Objects")]
    public GameObject folderMenu;

    [Header("Variables")]
    public float openTime = 1f;

    void Start()
    {
        OpenFolder();
    }


    void Update()
    {
        
    }

    private void OpenFolder()
    {
        folderMenu.SetActive(false);

        StartCoroutine("ShowMenu");
    }

    private IEnumerator ShowMenu()
    {
        while (true)
        {
            yield return new WaitForSeconds(openTime);

            folderMenu.SetActive(true);
        }
    }
}
