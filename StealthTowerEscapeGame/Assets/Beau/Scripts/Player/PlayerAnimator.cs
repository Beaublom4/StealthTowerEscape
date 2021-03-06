using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Melee meleeScript;
	public void AnimHit()
    {
        meleeScript.DoHit();
    }
}