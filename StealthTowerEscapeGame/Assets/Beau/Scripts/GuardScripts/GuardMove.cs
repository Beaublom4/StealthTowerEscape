using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMove : MonoBehaviour
{
	[SerializeField] GuardPath walkingPath;

	public float walkingSpeed, searchingTime;

    [SerializeField] Animator anim;
    [HideInInspector] public NavMeshAgent agent;
    int currentDest;
    Transform currentDestTrans;

    Transform playerLoc;
    Vector3 lastPlayerLoc;

    public bool spottedPlayer, lostPlayer;
    public bool isAttacking;

    [SerializeField] GameObject spotSignObj;
    IEnumerator coroutine;

    IEnumerator whistle;

    [SerializeField] GameObject shootTrigger;
    [SerializeField] Light flashLight;
    [SerializeField] Color spottedColor;

    bool Stunned;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkingSpeed;
    }
    private void Start()
    {
        SetUp();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if(hit.collider.tag == "Door")
            {
                if (!hit.collider.GetComponent<Door>().open)
                {
                    hit.collider.GetComponent<Door>().StartOpenCoroutine();
                }
            }
        }

        if (Stunned)
            return;

        if (spottedPlayer == true)
        {
            Quaternion lR = Quaternion.LookRotation((new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z) - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lR, 3 * Time.deltaTime);
        }
        if (isAttacking == true)
        {
            Quaternion lR = Quaternion.LookRotation((new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z) - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lR, 3 * Time.deltaTime);
            agent.SetDestination(playerLoc.transform.position);
        }
    }
    void SetUp()
    {
        if (walkingPath != null)
        {
            anim.SetBool("Walking", true);
            agent.SetDestination(walkingPath.points[currentDest].transform.position);
            currentDestTrans = walkingPath.points[currentDest].transform;
        }
        else
        {
            //idle
        }
    }

    public void NextLocation(Transform point)
    {
        if (spottedPlayer == true || isAttacking == true || lostPlayer == true)
            return;
        if (walkingPath.points[currentDest].transform != point)
            return;

        currentDest++;
        if (currentDest >= walkingPath.points.Length)
        {
            currentDest = 0;
        }
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
    }

    public void SpottedPlayer(Transform _player)
    {
        anim.SetBool("Walking", false);
        agent.isStopped = true;
        playerLoc = _player;
        spottedPlayer = true;
    }
    public void StopSearchForPlayer()
    {
        print("Lost player");
        isAttacking = false;
        spottedPlayer = false;

        shootTrigger.GetComponent<GuardShoot>().StopShooting();
        shootTrigger.SetActive(false);

        lastPlayerLoc = playerLoc.position;
        agent.SetDestination(lastPlayerLoc);
        agent.isStopped = false;
    }
    public void AttackPlayer(GameObject _player)
    {
        if (isAttacking)
            return;
        print("Attack player");
        playerLoc = _player.transform;

        anim.SetBool("Walking", true);
        anim.SetBool("Attacking", true);

        isAttacking = true;
        spottedPlayer = false;

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = ShowMark();
        StartCoroutine(coroutine);

        flashLight.color = spottedColor;

        shootTrigger.SetActive(true);

        agent.isStopped = false;
    }
    IEnumerator ShowMark()
    {
        spotSignObj.SetActive(true);
        yield return new WaitForSeconds(2);
        spotSignObj.SetActive(false);
    }
    public void Whistle(Vector3 pos, float whistleTime)
    {
        print("Guard: " + gameObject.name + " got whistled");

        if(whistle != null)
            StopCoroutine(whistle);
        whistle = ReturnAfterWhistle(whistleTime);
        StartCoroutine(whistle);

        agent.SetDestination(pos);
    }
    IEnumerator ReturnAfterWhistle(float whistleTime)
    {
        yield return new WaitForSeconds(whistleTime);
        ReturnToPath();
    }
    public void ReturnToPath()
    {
        print("Return on path");
        anim.SetBool("Attacking", false);
        flashLight.color = Color.white;
        spottedPlayer = false;
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
    }

    public GameObject icon;
    IEnumerator iconCoroutine;
    public void ShowEnemy()
    {
        if (iconCoroutine != null)
            StopCoroutine(iconCoroutine);
        iconCoroutine = ShowEnemyIcon();
        StartCoroutine(iconCoroutine);
    }
    IEnumerator ShowEnemyIcon()
    {
        icon.SetActive(true);
        yield return new WaitForSeconds(30);
        icon.SetActive(false);
    }

    IEnumerator coroutineDistort;
    public void Distort()
    {
        if (coroutineDistort != null)
            StopCoroutine(coroutineDistort);
        coroutineDistort = DistortTime();
        StartCoroutine(coroutineDistort);
    }
    IEnumerator DistortTime()
    {
        print("stunned");

        Stunned = true;
        agent.isStopped = true;
        anim.SetBool("Walking", false);
        GetComponentInChildren<GuardVision>().enabled = false;

        yield return new WaitForSeconds(10);

        print("not stunned any more");
    }
}