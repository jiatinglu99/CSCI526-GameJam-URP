using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseMonsterController : MonoBehaviour
{
    public static int velo = 50;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTransform.position);
    }

}