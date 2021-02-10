using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    [SerializeField] float spotSpeed;
    [SerializeField] float fieldOfView;

    [SerializeField] Transform lookPos;
    [SerializeField] Transform[] lookPosVisualLine;
    float visionRange;

    public bool playerInSight;

    Color color;

    private void Awake()
    {
        lookPosVisualLine[0].localRotation = Quaternion.Euler(0, fieldOfView * .5f, 0);
        lookPosVisualLine[1].localRotation = Quaternion.Euler(0, -fieldOfView * .5f, 0);
        lookPosVisualLine[2].localRotation = Quaternion.Euler(fieldOfView * .5f, 0, 0);
        lookPosVisualLine[3].localRotation = Quaternion.Euler(-fieldOfView * .5f, 0, 0);
        visionRange = GetComponent<SphereCollider>().radius;
    }
    private void Update()
    {
        Vector3 lookPos1 = (lookPosVisualLine[0].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos1, Color.red);
        Vector3 lookPos2 = (lookPosVisualLine[1].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos2, Color.red);
        Vector3 lookPos3 = (lookPosVisualLine[2].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos3, Color.red);
        Vector3 lookPos4 = (lookPosVisualLine[3].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos4, Color.red);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            color = Color.white;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            float angle = (Vector3.Angle(direction, transform.forward));

            if (angle < fieldOfView * .5f)
            {
                color = Color.blue;

                RaycastHit hit;
                if (Physics.Raycast(lookPos.position, other.transform.position - lookPos.position, out hit, 100, -5, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Player")
                    {
                        color = Color.green;

                        if (playerInSight == false)
                        {
                            print("spotted");
                            playerInSight = true;
                            other.GetComponent<PlayerGetSpotted>().IncreaseSpottedMeter(spotSpeed, transform.parent.gameObject);
                        }
                    }
                    else if (playerInSight == true)
                    {
                        TurnPlayerInSightFalse(other.gameObject);
                    }
                }
                else if (playerInSight == true)
                {
                    TurnPlayerInSightFalse(other.gameObject);
                }
            }
            else if (playerInSight == true)
            {
                TurnPlayerInSightFalse(other.gameObject);
            }
            DrawVisionRay(other.gameObject);
        }
    }
    void DrawVisionRay(GameObject player)
    {
        Debug.DrawRay(lookPos.position, player.transform.position - lookPos.position, color);
    }
    void TurnPlayerInSightFalse(GameObject player)
    {
        playerInSight = false;
        player.GetComponent<PlayerGetSpotted>().DecreaseSpottedMeter();
    }
    public void AttackPlayer()
    {
        print("Attack player by cam");
    }
}