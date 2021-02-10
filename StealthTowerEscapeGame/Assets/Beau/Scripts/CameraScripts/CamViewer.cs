using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamViewer : MonoBehaviour
{
	[SerializeField] Material[] camMats;
	[SerializeField] int camNum;

    [SerializeField] Material currentMat;
    private void Awake()
    {
        camNum = Random.Range(0, camMats.Length);
        currentMat = camMats[camNum];
    }
}