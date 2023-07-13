using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseMonsterController : MonoBehaviour
{
    public static int velo = 50;
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform playerTransform;

    private bool startedChasing = false;

    void Start()
    {
        StartCoroutine(StartChase());
    }

    // Update is called once per frame
    void Update()
    {
        if (startedChasing)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    IEnumerator StartChase()
    {
        yield return new WaitForSeconds(5);
        startedChasing = true;
    }
}