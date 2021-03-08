using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    public Transform playerLoc;
    float offset;
    private void Awake()
    {
        offset = playerLoc.position.y + transform.position.y;
    }
    private void Update()
    {
        transform.position = playerLoc.position + new Vector3(0, offset, 0);
    }
}