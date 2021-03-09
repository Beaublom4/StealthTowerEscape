using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    [SerializeField] float spotSpeed;
    [SerializeField] float fieldOfView;
    [SerializeField] Light spot;

    [SerializeField] Transform lookPos;
    [SerializeField] Transform[] lookPosVisualLine;
    float visionRange;

    public bool playerInSight;

    Color color;

    [SerializeField] LayerMask mask, guardMask;

    [SerializeField] GameObject interactParticle;
    [SerializeField] Animator anim;
    IEnumerator coroutineRedCam;

    public bool disabled;

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
        if (disabled)
            return;

        if (other.tag == "Player")
        {
            color = Color.white;
            Vector3 direction = (other.transform.position - lookPos.position).normalized;
            float angle = (Vector3.Angle(direction, lookPos.transform.forward));

            if (angle < fieldOfView * .5f)
            {
                color = Color.blue;

                RaycastHit hit;
                if (Physics.Raycast(lookPos.position, other.transform.position - lookPos.position, out hit, 20, mask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Player")
                    {
                        color = Color.green;

                        if(!hit.collider.GetComponent<PlayerGetSpotted>().isSpotted)
                        {
                            playerInSight = true;
                            hit.collider.GetComponent<PlayerGetSpotted>().IncreaseSpottedMeter(spotSpeed);
                        }
                        else
                        {
                            if (coroutineRedCam != null)
                                StopCoroutine(coroutineRedCam);
                            coroutineRedCam = RedCam();
                            StartCoroutine(coroutineRedCam);

                            Collider[] guards = Physics.OverlapSphere(transform.position, 15, guardMask);
                            foreach (Collider guard in guards)
                            {
                                if (guard.tag != "Enemy")
                                    continue;
                                guard.GetComponent<GuardMove>().AttackPlayer(other.gameObject);
                            }
                        }
                    }
                    else if (playerInSight)
                    {
                        PlayerOutVision(other.gameObject);
                    }
                }
                else if (playerInSight)
                {
                    PlayerOutVision(other.gameObject);
                }
            }
            else if (playerInSight)
            {
                PlayerOutVision(other.gameObject);
            }
            DrawVisionRay(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        if (playerInSight)
        {
            PlayerOutVision(other.gameObject);
        }
    }

    IEnumerator RedCam()
    {
        spot.color = Color.red;
        yield return new WaitForSeconds(5);
        spot.color = Color.white;
    }
    void PlayerOutVision(GameObject player)
    {
        playerInSight = false;
        player.GetComponent<PlayerGetSpotted>().DecreaseSpottedMeter();
    }
    void DrawVisionRay(GameObject player)
    {
        Debug.DrawRay(lookPos.position, player.transform.position - lookPos.position, color);
    }
    public void CamDisable(float disableTime)
    {
        if (!disabled)
        {
            disabled = true;
            StartCoroutine(CoroutineDisableCam(disableTime));
        }
    }
    IEnumerator CoroutineDisableCam(float time)
    {
        Destroy(Instantiate(interactParticle, lookPos.position, Quaternion.identity, null), 3);
        anim.SetBool("Disabled", true);
        spot.enabled = false;
        yield return new WaitForSeconds(time);
        anim.SetBool("Disabled", false);
        spot.enabled = true;
        disabled = false;
    }
}