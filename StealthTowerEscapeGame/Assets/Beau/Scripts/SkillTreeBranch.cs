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

    public void UnlockBranch()
    {
        unlocked = true;
        print("Unlucked");
    }
}