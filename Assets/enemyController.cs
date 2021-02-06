using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public GameObject target;
    public bool isDead;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!isDead)
            _agent.destination = target.transform.position;
    }
}
