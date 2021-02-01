using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointCol : MonoBehaviour
{
    GuardMove guardScript;
    private void Awake()
    {
        guardScript = GetComponentInParent<GuardMove>();       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PathPoint")
            guardScript.NextLocation(other.transform);
    }
}