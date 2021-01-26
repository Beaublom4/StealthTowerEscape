using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBranch : MonoBehaviour
{
    public Slider branchSlider;

    [SerializeField] public SkillTreeBranch preBranch;
    [SerializeField] public bool unlocked;

    public void UnlockBranch()
    {
        unlocked = true;
        print("Unlucked");
    }
}