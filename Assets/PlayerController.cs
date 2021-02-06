using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    
    [SerializeField]
    public float speed = 10f;

    public float attackRadius = 50;
    
    [SerializeField] 
    private GameObject bow;

    private NavMeshAgent _agent;
    private Animator _animator;
    
    private bool _isWalking;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (transform.position == target.transform.position)
        {
            _isWalking = false;
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsRunning, false);
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag != "Player")
                {
                    _animator.SetBool(IsWalking, false);
                    _animator.SetBool(IsRunning, false);
                    var targetPosition = hit.point;
                    target.transform.position = targetPosition;
                    _isWalking = true;
                }
            }
        }
    }
    
    float reloadTime = 500;
    private void FixedUpdate()
    {
        if (_isWalking)
        {
            MoveLogic();
        }
        else
        {
            reloadTime -= Time.fixedDeltaTime * 1000;
            if (reloadTime <= 0)
            {
                ShootLogic();
                reloadTime = 500;
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        GameObject closest = null;
        
        foreach (var enemy in enemies)
        {
            if (enemy.GetComponent<enemyController>().isDead)
                continue;
            
            var distanceToEnemy = Vector3.Distance(transform.position,
                enemy.transform.position);
            
            if(distanceToEnemy > attackRadius)
                continue;
            
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }

        return closest;
    }
    
    void ShootLogic()
    {
        var closestEnemy = FindClosestEnemy();
        
        if (closestEnemy == null)
            return;
        
        bow.GetComponent<shootingController>().SpawnArrow(gameObject, closestEnemy.transform.position);
    }
    
    void MoveLogic()
    {
        var movementVector = Vector3.Lerp(transform.position, 
            target.transform.position, Time.fixedDeltaTime * speed);
            
        var diff = (movementVector - transform.position).magnitude;
        diff = Mathf.Clamp(diff, 0, 1);
        
        if (diff < 0.05)
        {
            _animator.SetBool(IsWalking, false);
            _animator.SetBool(IsRunning, false);
            _isWalking = false;
        }
        else if (diff > 0.05 && diff < 0.3f)
        {
            _animator.SetBool(IsWalking, true);
            _animator.SetBool(IsRunning, false);
        }
        else
        {
            _animator.SetBool(IsWalking, true);
            _animator.SetBool(IsRunning, true);
        }

        _agent.destination = target.transform.position;
    }
}
