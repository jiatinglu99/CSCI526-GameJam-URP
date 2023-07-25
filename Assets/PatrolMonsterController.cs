using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMonsterController : MonoBehaviour
{
    enum State
    {
        PATROL,
        CHASE,
        FREEZE
    }
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform startTransform;
    public Transform endTransform;

    private bool goingToStart = true;
    private State state;
    private float effectTimer = 0f;
    private GameObject player;

    private static Color whiteLight = new Color32(255, 255, 255, 255);
    private static Color blueLight = new Color32(0, 100, 255, 255);
    private static Color redLight = new Color32(255, 30, 0, 255);

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        state = State.PATROL;
        agent.SetDestination(endTransform.position);
    }

    void Update()
    {
        if (state == State.PATROL)
        {
            PatrolUpdate();
        }
        else if (state == State.CHASE)
        {
            ChaseUpdate();
        }
        else if (state == State.FREEZE)
        {
            FreezeUpdate();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Flashlight")
        {
            if (other.gameObject.GetComponent<Light>().color == redLight)
            {
                state = State.CHASE;
                effectTimer = 6f;
            }
            else if (other.gameObject.GetComponent<Light>().color == blueLight)
            {
                state = State.FREEZE;
                effectTimer = 1f;
            }
        }
    }

    void FreezeUpdate()
    {
        effectTimer -= Time.deltaTime;
        if (effectTimer <= 0f)
        {
            state = State.PATROL;
            GoToStart();
            return;
        }
        agent.SetDestination(transform.position);
    }

    void ChaseUpdate()
    {
        effectTimer -= Time.deltaTime;
        if (effectTimer <= 0f)
        {
            state = State.PATROL;
            GoToStart();
            return;
        }
        agent.SetDestination(player.transform.position);
    }
    void PatrolUpdate()
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
