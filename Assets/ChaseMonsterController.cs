using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseMonsterController : MonoBehaviour
{
    enum State{
        CHASE,
        FREEZE
    }
    public static int velo = 50;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;

    private State state;
    private float effectTimer = 0f;

    private static Color whiteLight = new Color32(255, 255, 255, 255);
    private static Color blueLight = new Color32(0, 100, 255, 255);
    private static Color redLight = new Color32(255, 30, 0, 255);

    void Start()
    {
        state = State.FREEZE;
        effectTimer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.CHASE)
        {
            agent.SetDestination(playerTransform.position);
        }
        else if (state == State.FREEZE)
        {
            FreezeUpdate();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Flashlight")
        {
            if (other.gameObject.GetComponent<Light>().color == blueLight)
            {
                state = State.FREEZE;
                effectTimer = 1f;
            }
        }
    }

    void FreezeUpdate()
    {
        effectTimer -= Time.deltaTime;
        if (effectTimer <= 0)
        {
            state = State.CHASE;
            return;
        }
        agent.SetDestination(transform.position);
    }
}