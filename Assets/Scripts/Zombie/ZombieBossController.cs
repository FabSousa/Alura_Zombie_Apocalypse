using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieBossController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Transform playerPos;

    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindWithTag(Strings.PlayerTag).transform;
    }

    private void Update(){
        navMeshAgent.SetDestination(playerPos.position);
    }
}
