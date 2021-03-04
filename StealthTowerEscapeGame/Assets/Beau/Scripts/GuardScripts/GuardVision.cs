using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    [SerializeField] float fieldOfView;
    [SerializeField] float spotSpeed;

    [SerializeField] Transform lookPos;
    [SerializeField] GameObject guardMain;

    [SerializeField] Transform[] lookPosVisualLine;
    float visionRange;
    Color color;

    GuardMove guardMoveScript;
    bool playerInSight;

    [SerializeField] float lostPlayerSearchTime;
    IEnumerator lostPlayerCoroutine;

    private void Awake()
    {
        guardMoveScript = GetComponentInParent<GuardMove>();
        lookPosVisualLine[0].localRotation = Quaternion.Euler(0, fieldOfView * .5f, 0);
        lookPosVisualLine[1].localRotation = Quaternion.Euler(0, -fieldOfView * .5f, 0);
        visionRange = GetComponent<SphereCollider>().radius;
    }
    private void Update()
    {
        Vector3 lookPos1 = (lookPosVisualLine[0].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos1, Color.red);
        Vector3 lookPos2 = (lookPosVisualLine[1].forward * visionRange) + lookPos.position;
        Debug.DrawLine(lookPos.position, lookPos2, Color.red);
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

                        if (!guardMoveScript.isAttacking)
                        {
                            if (playerInSight == false)
                            {
                                playerInSight = true;
                                print("Spotted");
                                other.GetComponent<PlayerGetSpotted>().IncreaseSpottedMeter(spotSpeed, guardMain);
                                guardMoveScript.SpottedPlayer(other.transform);
                            }
                        }
                        else
                        {
                            if (lostPlayerCoroutine != null)
                                StopCoroutine(lostPlayerCoroutine);
                        }
                    }
                    else if (playerInSight == true)
                    {
                        PlayerInSightToFalse(other.gameObject);
                    }
                }
                else if (playerInSight == true)
                {
                    PlayerInSightToFalse(other.gameObject);
                }
            }
            else if (playerInSight == true)
            {
                PlayerInSightToFalse(other.gameObject);
            }
            DrawVisionRay(other.gameObject);
        }
    }
    void DrawVisionRay(GameObject player)
    {
        Debug.DrawRay(lookPos.position, player.transform.position - lookPos.position, color);
    }
    void PlayerInSightToFalse(GameObject player)
    {
        playerInSight = false;
        player.GetComponent<PlayerGetSpotted>().DecreaseSpottedMeter();

        if(guardMoveScript.spottedPlayer == true)
        {
            guardMoveScript.spottedPlayer = false;
            if (lostPlayerCoroutine != null)
                StopCoroutine(lostPlayerCoroutine);
            lostPlayerCoroutine = LostPlayerVision();
            StartCoroutine(lostPlayerCoroutine);
        }
        else if(guardMoveScript.isAttacking == true)
        {
            guardMoveScript.isAttacking = false;
            if (lostPlayerCoroutine != null)
                StopCoroutine(lostPlayerCoroutine);
            lostPlayerCoroutine = LostPlayerVision();
            StartCoroutine(lostPlayerCoroutine);
        }
    }
    IEnumerator LostPlayerVision()
    {
        guardMoveScript.lostPlayer = true;
        guardMoveScript.StopSearchForPlayer();
        yield return new WaitForSeconds(lostPlayerSearchTime);
        guardMoveScript.lostPlayer = false;
        guardMoveScript.ReturnToPath();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerInSightToFalse(other.gameObject);
        }
    }
}