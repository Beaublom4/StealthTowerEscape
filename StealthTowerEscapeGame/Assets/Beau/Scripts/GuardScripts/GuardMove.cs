using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMove : MonoBehaviour
{
	[SerializeField] GuardPath walkingPath;

	[SerializeField] float walkingSpeed, runningSpeed;

    NavMeshAgent agent;
    int currentDest;
    Transform currentDestTrans;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkingSpeed;
    }
    private void Start()
    {
        SetUp();
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

    public void SetPlayerAsLoc(GameObject player)
    {

    }
}