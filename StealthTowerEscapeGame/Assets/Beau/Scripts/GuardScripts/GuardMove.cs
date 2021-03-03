using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMove : MonoBehaviour
{
	[SerializeField] GuardPath walkingPath;

	[SerializeField] float walkingSpeed, runningSpeed, searchingTime;

    NavMeshAgent agent;
    int currentDest;
    Transform currentDestTrans;

    Transform playerLoc;
    Vector3 lastPlayerLoc;

    public bool spottedPlayer, lostPlayer;
    public bool isAttacking;

    IEnumerator whistle;

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
        if (spottedPlayer == true)
        {
            transform.LookAt(new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z));
        }
        if (isAttacking == true)
        {
            transform.LookAt(new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z));
            agent.SetDestination(playerLoc.transform.position);
        }
    }
    void SetUp()
    {
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
        currentDestTrans = walkingPath.points[currentDest].transform;
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
        agent.isStopped = true;
        playerLoc = _player;
        spottedPlayer = true;
    }
    public void StopSearchForPlayer()
    {
        print("Lost player");
        isAttacking = false;
        spottedPlayer = false;
        lastPlayerLoc = playerLoc.position;
        agent.SetDestination(lastPlayerLoc);
        agent.isStopped = false;
    }
    public void AttackPlayer(GameObject player)
    {
        print("Attack player");
        isAttacking = true;
        spottedPlayer = false;

        agent.isStopped = false;
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
        spottedPlayer = false;
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
    }
}