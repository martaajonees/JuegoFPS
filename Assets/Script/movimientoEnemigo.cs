using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movimientoEnemigo : MonoBehaviour
{
    NavMeshAgent pathfinder;
    Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        pathfinder = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        pathfinder.SetDestination(target.position);
    }
}
