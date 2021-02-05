using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public GameObject target;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        _agent.destination = target.transform.position;
    }
}
