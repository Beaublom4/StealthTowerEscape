using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
	[System.Serializable]
	public class Frame
    {
        [TextArea]
        public string speech;
        public GameObject image, afterTextImage;
        public bool isText;
        public float waitTime;
    }

    public int currentText;
    public Frame[] frames;
    public GameObject[] frameImages;

    public float talkingSpeed, fastTalkingSpeed;
    public float talkSpeed;
    float f;
    int currentCharacter;
    public TMP_Text text;

    bool waitingForNewLine = true;
    bool canSkip;

    public GameObject skip;

    private void Awake()
    {
        waitingForNewLine = true;
        talkSpeed = talkingSpeed;
    }
    private void Start()
    {
        currentText = 0;
        Invoke("NextLine", 1);
    }
    private void Update()
    {
        if (waitingForNewLine == false)
        {
            if (f < text.textInfo.characterCount)
            {
                f += talkSpeed * Time.deltaTime;
                currentCharacter = (int)f;
                text.maxVisibleCharacters = currentCharacter;
            }
            else
            {
                waitingForNewLine = true;
                ResetFrames();
                frames[currentText].afterTextImage.SetActive(true);
                print("nigro");
                canSkip = true;
            }
        }
        if (canSkip)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                canSkip = false;
                if (1 + currentText < frames.Length)
                {
                    currentText++;
                    NextLine();
                }
                else
                {
                    End();
                }
            }
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                talkSpeed = fastTalkingSpeed;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                talkSpeed = talkingSpeed;
            }
        }
    }
    public void NextLine()
    {
        if (frames[currentText].isText)
        {
            ResetFrames();
            text.maxVisibleCharacters = 0;
            f = 0;

            text.text = frames[currentText].speech;
            text.gameObject.SetActive(true);
            frames[currentText].image.SetActive(true);
            talkSpeed = talkingSpeed;

            waitingForNewLine = false;
        }
        else
        {
            ResetFrames();
            text.gameObject.SetActive(false);
            frames[currentText].image.SetActive(true);
            Invoke("WaitForNext", frames[currentText].waitTime);
        }
    }
    void ResetFrames()
    {
        foreach(GameObject g in frameImages)
        {
            g.SetActive(false);
        }
    }
    void WaitForNext()
    {
        currentText++;
        NextLine();
    }

    public void Skip()
    {
        skip.SetActive(true);
        Time.timeScale = 0;
    }
    public void Yes()
    {
        End();
    }
    public void No()
    {
        Time.timeScale = 1;
        skip.SetActive(false);
    }

    public void End()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}