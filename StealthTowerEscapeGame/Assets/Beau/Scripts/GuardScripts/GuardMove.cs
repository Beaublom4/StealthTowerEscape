using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMove : MonoBehaviour
{
	[SerializeField] GuardPath walkingPath;

	[SerializeField] float walkingSpeed, runningSpeed, searchingTime;
    [SerializeField] Transform lastPlayerPos;

    NavMeshAgent agent;
    int currentDest;
    Transform currentDestTrans;

    Transform playerLoc;
    public bool lookForPlayer;
    IEnumerator searchCoroutine;

    public bool isAttacking;

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
        if(lookForPlayer == true)
        {
            transform.LookAt(new Vector3(playerLoc.position.x, transform.position.y, playerLoc.position.z));
        }
        if(isAttacking == true)
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
        if (walkingPath.points[currentDest].transform != point)
            return;

        currentDest++;
        if (currentDest >= walkingPath.points.Length)
        {
            currentDest = 0;
        }
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
    }

    public void SearchForPlayer(Transform _player)
    {
        if (searchCoroutine != null)
            StopCoroutine(searchCoroutine);
        agent.isStopped = true;
        playerLoc = _player;
        lookForPlayer = true;
    }
    public void StopSearchForPlayer()
    {
        if (searchCoroutine != null)
            StopCoroutine(searchCoroutine);
        searchCoroutine = LookForPlayerTime();
        StartCoroutine(searchCoroutine);
    }
    public void AttackPlayer(GameObject player)
    {
        print("Attack player");
        agent.isStopped = false;
        lookForPlayer = false;
        isAttacking = true;
    }
    IEnumerator LookForPlayerTime()
    {
        lookForPlayer = false;
        agent.SetDestination(playerLoc.position);
        agent.isStopped = false;
        yield return new WaitForSeconds(searchingTime);
        ReturnToPath();
    }
    public void ReturnToPath()
    {
        print("Return on path");
        agent.SetDestination(walkingPath.points[currentDest].transform.position);
    }
}