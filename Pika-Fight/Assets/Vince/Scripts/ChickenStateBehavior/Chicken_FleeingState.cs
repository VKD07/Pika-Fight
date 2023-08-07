using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Chicken_FleeingState : ChickenState
{
    Vector3 direction;
    Vector3 velocity;
    Vector3 fleeDestination;
    public override void OnEnterState(ChickenStateManager state)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdateState(ChickenStateManager state)
    {
        Debug.Log("Fleeing State");
        FleeMovement(state.transform, state.Players, state.navAgent, state.WalkRange);

        //if (state.navAgent.remainingDistance <= state.navAgent.stoppingDistance)
        //{
        //    Debug.Log("Done Path");
        //    state.navAgent.ResetPath();
        //    state.switchState(state.WalkingState);
        //}
    }

    public override void OnCollisionEnter(ChickenStateManager state)
    {
    }

    void FleeMovement(Transform currentPos, Collider [] players, NavMeshAgent agent, float moveDistance)
    {
        //Vector3 direction = currentPos.position - player.position;
        //direction.Normalize();
        //Vector3 fleeDestination = currentPos.position + direction * moveDistance;
        //agent.SetDestination(fleeDestination);
        Vector3 cumulativeFleeDirection = new Vector3();

        foreach (Collider obstacle in players)
        {
            Vector3 directionToObstacle = currentPos.position - obstacle.transform.position;
            directionToObstacle.Normalize();

            cumulativeFleeDirection += directionToObstacle;
        }

        cumulativeFleeDirection.Normalize();

        Vector3 fleeDestination = currentPos.position + cumulativeFleeDirection * moveDistance;
        agent.SetDestination(fleeDestination);
    }
}
