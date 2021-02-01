using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    [System.Serializable]
    public class Stats
    {
        public float[] stealthNum;
        public float[] throwingDistNum;
        public float[] camVisability;
        public float[] damageNum;
        public float[] grabGuardSpeedNum;
        public float[] speedNum;
        public float[] crouchWalkSpeedNum;
        public float[] slideSpeedNum;
        public float[] slideRangeNum;
    }
    public Stats stats;

    public Color unlockedColor;

    [SerializeField] float sliderSpeed;
    [SerializeField] SkillTreeBranch currentBranchScript;
    [SerializeField] GameObject hoverInfoPanel, skillTreePanel;
    [SerializeField] float zoomSpeed, minClamp, maxClamp;

    float mouseScroll;
    public bool increaseSlider;
    private void Update()
    {
        mouseScroll = Input.mouseScrollDelta.y;
        if (mouseScroll > 0)
        {
            //zoom out
            float newScale = skillTreePanel.transform.localScale.x;
            newScale += zoomSpeed * Time.deltaTime;
            newScale = Mathf.Clamp(newScale, minClamp, maxClamp);
            skillTreePanel.transform.localScale = new Vector3(newScale, newScale, skillTreePanel.transform.localScale.z);
        }
        else if (mouseScroll < 0)
        {
            //zoom in
            float newScale = skillTreePanel.transform.localScale.x;
            newScale -= zoomSpeed * Time.deltaTime;
            newScale = Mathf.Clamp(newScale, minClamp, maxClamp);
            skillTreePanel.transform.localScale = new Vector3(newScale, newScale, skillTreePanel.transform.localScale.z);
        }

        if (increaseSlider == true)
        {
            currentBranchScript.branchSlider.value += sliderSpeed * Time.deltaTime;
            if(currentBranchScript.branchSlider.value == 1)
            {
                if (currentBranchScript.unlocked)
                    return;
                increaseSlider = false;
                currentBranchScript.anim.SetBool("Pressing", false);
                currentBranchScript.gameObject.GetComponent<Image>().color = unlockedColor;
                currentBranchScript.UnlockBranch();
                currentBranchScript = null;
            }
        }
    }
    public void OnButtonDown(SkillTreeBranch branchScript)
    {
        currentBranchScript = branchScript;
        if (currentBranchScript.unlocked == false && currentBranchScript.preBranch.unlocked == true)
        {
            currentBranchScript.anim.SetBool("Pressing", true);
            increaseSlider = true;
        }
    }
    public void OnButtonUp()
    {
        if (currentBranchScript == null)
            return;
        if (currentBranchScript.unlocked && currentBranchScript.preBranch.unlocked)
            return;

        currentBranchScript.anim.SetBool("Pressing", false);
        increaseSlider = false;
        currentBranchScript.branchSlider.value = 0;
        currentBranchScript = null;
    }
    public void OnHoverEnter(SkillTreeBranch branchScript)
    {
        hoverInfoPanel.GetComponentInChildren<TMP_Text>().text = branchScript.info;
        hoverInfoPanel.SetActive(true);
    }
    public void OnHoverExit()
    {
        hoverInfoPanel.SetActive(false);
        hoverInfoPanel.GetComponentInChildren<TMP_Text>().text = "";
    }
}
