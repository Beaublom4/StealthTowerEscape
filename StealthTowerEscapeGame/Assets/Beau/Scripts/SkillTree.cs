using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField] float sliderSpeed;
    [SerializeField] SkillTreeBranch currentBranchScript;

    bool increaseSlider;
    private void Update()
    {
        if(increaseSlider == true)
        {
            currentBranchScript.branchSlider.value += sliderSpeed * Time.deltaTime;
            if(currentBranchScript.branchSlider.value == 1)
            {
                if (currentBranchScript.unlocked)
                    return;
                currentBranchScript.UnlockBranch();
            }
        }
    }
    public void OnButtonDown(SkillTreeBranch branchScript)
    {
        currentBranchScript = branchScript;
        if (currentBranchScript.unlocked || !currentBranchScript.preBranch.unlocked)
            return;

        increaseSlider = true;
    }
    public void OnButtonUp()
    { 

    }
}
