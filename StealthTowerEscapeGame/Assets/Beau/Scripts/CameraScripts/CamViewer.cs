using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamViewer : MonoBehaviour
{
	[SerializeField] Material[] camMats;
	[SerializeField] int camNum;

    [SerializeField] MeshRenderer renderer;
    private void Awake()
    {
        camNum = Random.Range(0, camMats.Length);
        renderer.material = camMats[camNum];
    }
    public void ChangeCam()
    {
        camNum++;
        if (camNum >= camMats.Length)
            camNum = 0;

        renderer.material = camMats[camNum];
    }
}