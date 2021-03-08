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

    [SerializeField] LayerMask mask;

    GuardMove guardMoveScript;
    bool playerInSight;

    [SerializeField] float lostPlayerSearchTime;
    IEnumerator coroutineLostPlayer;

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

            if(angle < fieldOfView * .5f)
            {
                color = Color.blue;

                RaycastHit hit;
                if(Physics.Raycast(lookPos.position, other.transform.position - lookPos.position, out hit, 20, mask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Player")
                    {
                        color = Color.green;

                        if (!guardMoveScript.isAttacking)
                        {
                            playerInSight = true;
                            other.GetComponent<PlayerGetSpotted>().IncreaseSpottedMeter(spotSpeed);
                            guardMoveScript.SpottedPlayer(other.transform);
                            if(other.GetComponent<PlayerGetSpotted>().spottedMeter >= 100)
                            {
                                guardMoveScript.AttackPlayer(other.gameObject);
                            }
                        }
                    }
                    else if(playerInSight)
                    {
                        PlayerOutSight(other.gameObject);
                    }
                }
                else if (playerInSight)
                {
                    PlayerOutSight(other.gameObject);
                }
            }
            else if(playerInSight)
            {
                PlayerOutSight(other.gameObject);
            }
            DrawVisionRay(other.gameObject);
        }

        #region Vision
        /*
        if (other.tag == "Player")
        {
            color = Color.white;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            float angle = (Vector3.Angle(direction, transform.forward));

            if (angle < fieldOfView * .5f)
            {
                color = Color.blue;

                RaycastHit hit;
                if (Physics.Raycast(lookPos.position, other.transform.position - lookPos.position, out hit, 100, mask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Player")
                    {
                        color = Color.green;

                        if (!guardMoveScript.isAttacking)
                        {
                            playerInSight = true;
                            print("Spotted");
                            other.GetComponent<PlayerGetSpotted>().IncreaseSpottedMeter(spotSpeed, guardMain);
                            guardMoveScript.SpottedPlayer(other.transform);
                            if(hit.collider.GetComponent<PlayerGetSpotted>().spottedMeter >= 100)
                            {
                                guardMain.GetComponent<GuardMove>().AttackPlayer(other.gameObject);
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
        */
        #endregion
    }
    void DrawVisionRay(GameObject player)
    {
        Debug.DrawRay(lookPos.position, player.transform.position - lookPos.position, color);
    }
    void PlayerOutSight(GameObject player)
    {
        if (coroutineLostPlayer != null)
            StopCoroutine(coroutineLostPlayer);
        coroutineLostPlayer = LostPlayerVision(player);
        StartCoroutine(coroutineLostPlayer);
    }
    IEnumerator LostPlayerVision(GameObject player)
    {
        playerInSight = false;
        player.GetComponent<PlayerGetSpotted>().DecreaseSpottedMeter();
        yield return new WaitForSeconds(2);
        guardMoveScript.StopSearchForPlayer();
        yield return new WaitForSeconds(lostPlayerSearchTime);
        guardMoveScript.ReturnToPath();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!guardMoveScript.isAttacking)
            return;
        if(other.tag == "Player")
        {
            PlayerOutSight(other.gameObject);
        }
    }
}