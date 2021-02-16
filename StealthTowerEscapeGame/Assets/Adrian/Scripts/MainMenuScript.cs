using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [Header("Other Scripts")]
    public FolderScript folderScript;

    [Header("Objects")]
    public GameObject cam;
    public GameObject gameTitleCanvas;
    public GameObject folderCanvas;
    public GameObject boardCanvas;
    public GameObject folderButton;
    public Transform fullCamView;
    public Transform folderCamView;
    public Transform boardCamView;

    [Header("Variables")]
    public float transitionSpeed = 0.012f;
    public float transitionTime = 2.5f;

    public bool canTransitionToFullView;
    public bool canTransitionToFolderView;
    public bool canTransitionToBoardView;

    void Start()
    {
        gameTitleCanvas.SetActive(true);
        folderButton.SetActive(true);
        boardCanvas.SetActive(false);
        folderCanvas.SetActive(false);

        canTransitionToFullView = false;
        canTransitionToFolderView = false;
        canTransitionToBoardView = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TransitionToFolder();
        }

        FullCameraView();
        FolderCameraView();
        BoardCameraView();
    }

    public void FullCameraView()
    {
        if (canTransitionToFullView)
        {
            Vector3 fullCamViewPos = fullCamView.position;
            Quaternion fullCamViewRot = fullCamView.rotation;
            Vector3 moveCamToFullCamView = Vector3.Lerp(cam.transform.position, fullCamViewPos, transitionSpeed);
            Quaternion rotateCamToFullView = Quaternion.Lerp(cam.transform.rotation, fullCamViewRot, transitionSpeed);

            cam.transform.position = moveCamToFullCamView;
            cam.transform.rotation = rotateCamToFullView;
        }
    }

    public void FolderCameraView()
    {
        if (canTransitionToFolderView)
        {
            Vector3 folderCamViewPos = folderCamView.position;
            Quaternion folderCamViewRot = folderCamView.rotation;
            Vector3 moveCamToFolderCamView = Vector3.Lerp(cam.transform.position, folderCamViewPos, transitionSpeed);
            Quaternion rotateCamToBoardView = Quaternion.Lerp(cam.transform.rotation, folderCamViewRot, transitionSpeed);

            cam.transform.position = moveCamToFolderCamView;
            cam.transform.rotation = rotateCamToBoardView;
        }
    }

    public void BoardCameraView()
    {
        if (canTransitionToBoardView)
        {
            Vector3 boardCamViewPos = boardCamView.position;
            Quaternion boardCamViewRot = boardCamView.rotation;
            Vector3 moveCamToBoardCamView = Vector3.Lerp(cam.transform.position, boardCamViewPos, transitionSpeed);
            Quaternion rotateCamToBoardView = Quaternion.Lerp(cam.transform.rotation, boardCamViewRot, transitionSpeed);

            cam.transform.position = moveCamToBoardCamView;
            cam.transform.rotation = rotateCamToBoardView;
        }
    }

    public void TransitionToFolder()
    {
        folderScript.OpenFolder();

        gameTitleCanvas.SetActive(false);
        folderButton.SetActive(false);
        folderCanvas.SetActive(true);
        boardCanvas.SetActive(false);

        canTransitionToFullView = false;
        canTransitionToFolderView = true;
        canTransitionToBoardView = false;

        if (cam.transform.position == folderCamView.position && cam.transform.rotation == folderCamView.rotation)
        {
            canTransitionToFolderView = false;
        }
    }

    public void TransitionToBoard()
    {
        folderScript.CloseFolder();

        boardCanvas.SetActive(true);
        folderCanvas.SetActive(false);

        canTransitionToFullView = false;
        canTransitionToFolderView = false;
        canTransitionToBoardView = true;

        if (cam.transform.position == boardCamView.position && cam.transform.rotation == boardCamView.rotation)
        {
            canTransitionToBoardView = false;
        }
    }
}
