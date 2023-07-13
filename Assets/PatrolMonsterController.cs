using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMonsterController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform startTransform;
    public Transform endTransform;

    private bool goingToStart = true;

    void Start()
    {
        agent.SetDestination(endTransform.position);
    }

    void Update()
    {
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            if (goingToStart)
            {
                GoToEnd();
            }
            else
            {
                GoToStart();
            }
        }
    }

    void GoToStart()
    {
        agent.SetDestination(startTransform.position + new Vector3(0,40,0));
        goingToStart = true;
    }

    void GoToEnd()
    {
        agent.SetDestination(endTransform.position + new Vector3(0,40,0));
        goingToStart = false;
    }
}
