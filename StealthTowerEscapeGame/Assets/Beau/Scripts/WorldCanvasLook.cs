using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvasLook : MonoBehaviour
{
    Transform playerCamPos;
    private void Awake()
    {
        playerCamPos = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.LookAt(playerCamPos.position);
    }
}