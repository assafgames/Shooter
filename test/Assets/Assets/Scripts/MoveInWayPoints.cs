using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveInWayPoints : MonoBehaviour
{

    public Transform[] WayPoints;
    public Color WayPointColor = Color.red;
    private int curentWayPointIndex = 0;
    private NavMeshAgent navMeshAgent;
    private Animator anim;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        navMeshAgent.destination = WayPoints[curentWayPointIndex].position;
        Walk();
    }

    void Update()
    {
        // make the npc move to the next way point if needed
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
            {
                curentWayPointIndex += 1;
                if (curentWayPointIndex >= WayPoints.Length)
                {
                    curentWayPointIndex = 0;
                }
                navMeshAgent.destination = WayPoints[curentWayPointIndex].position;
            }
        }

    }

    public void Walk()
    {
        SetWalkingState(true);
    }

    public void StopWalking()
    {
        SetWalkingState(false);
    }

    private void SetWalkingState(bool walking)
    {
        navMeshAgent.isStopped = !walking;
        anim.SetBool("walk", walking);
    }

    void OnDrawGizmos()
    {
        if (WayPoints == null)
        {
            return;
        }
        Gizmos.color = WayPointColor;
        foreach (Transform wayPoint in WayPoints)
        {
            if (wayPoint != null)
                Gizmos.DrawWireCube(wayPoint.position, Vector3.one);
        }
    }

}
