using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken_WalkingState : ChickenState
{
    bool isWalking;
    float currentTime;
    public override void OnEnterState(ChickenStateManager state)
    {
    }

    public override void OnUpdateState(ChickenStateManager state)
    {
        Debug.Log("Walking State");
        WalkInRandomPos(state.navAgent, state.centerPos, state.WalkRange);
        WalkingDelayTimer(state.WalkDelay);
    }

    public override void OnCollisionEnter(ChickenStateManager state)
    {
    }

    void WalkInRandomPos(NavMeshAgent agent, Transform centerPoint, float walkingRange)
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !isWalking)
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, walkingRange, out point))
            {
                isWalking = true;
                Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void WalkingDelayTimer(float walkingDelay)
    {   
        if(currentTime < walkingDelay && isWalking)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            isWalking = false;
        }
    }
}
