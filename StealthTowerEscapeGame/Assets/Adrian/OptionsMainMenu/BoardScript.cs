using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardScript : MonoBehaviour
{
    [Header("Animator")]
    public Animator boardAnim;

    [Header("UI Elements")]
    public GameObject videoCanvas;
    public GameObject controlsCanvas;
    public GameObject audioCanvas;

    private void Start()
    {
        boardAnim.SetBool("CanRotate", false);

        videoCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        audioCanvas.SetActive(false);
    }

    public void BackToMainOptions()
    {
        boardAnim.SetBool("CanRotate", false);

        DisableCanvases();
    }

    public void VideoOptions()
    {
        boardAnim.SetBool("CanRotate", true);

        if (!videoCanvas.activeInHierarchy)
        {
            videoCanvas.SetActive(true);
        }
    }

    public void ControlsOptions()
    {
        boardAnim.SetBool("CanRotate", true);

        if (!controlsCanvas.activeInHierarchy)
        {
            controlsCanvas.SetActive(true);
        }
    }

    public void AudioControls()
    {
        boardAnim.SetBool("CanRotate", true);

        if (!audioCanvas.activeInHierarchy)
        {
            audioCanvas.SetActive(true);
        }
    }

    private void DisableCanvases()
    {
        if (videoCanvas.activeInHierarchy)
        {
            videoCanvas.SetActive(false);
        }
        if (controlsCanvas.activeInHierarchy)
        {
            controlsCanvas.SetActive(false);
        }
        if (audioCanvas.activeInHierarchy)
        {
            audioCanvas.SetActive(false);
        }
    }
}
