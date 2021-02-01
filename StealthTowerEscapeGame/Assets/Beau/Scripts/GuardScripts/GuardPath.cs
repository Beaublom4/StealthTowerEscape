using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPath : MonoBehaviour
{
	public GameObject[] points;

    private void Awake()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i).gameObject;
        }
    }
}