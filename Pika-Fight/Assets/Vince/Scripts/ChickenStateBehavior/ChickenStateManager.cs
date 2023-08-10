using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ChickenStateManager : MonoBehaviour
{
    [SerializeField] Transform centerPosition;
    [SerializeField] float initSpeed = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float walkingrange;
    [SerializeField] float walkDelay = 3f;
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] LayerMask obstaclesLayer;
    [SerializeField] Collider[] players;
    [SerializeField] GameObject chickenUI;
    bool chickenIsTaken;
    GameObject playerDetected;

    NavMeshAgent agent;
    ChickenState currentState;
    Chicken_FleeingState fleeingState = new Chicken_FleeingState();
    Chicken_WalkingState walkingState = new Chicken_WalkingState();
    public bool flee;
    void Start()
    {
        centerPosition = GameObject.Find("CenterOfTheMap").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = initSpeed;
        currentState = walkingState;
    }

    void Update()
    {
        currentState.OnUpdateState(this);
        DetectPlayers();
    }

    private void DetectPlayers()
    {
        players = Physics.OverlapSphere(transform.position, detectionRadius, obstaclesLayer);

        if (players.Length > 0)
        {
            agent.speed = maxSpeed;
            currentState = fleeingState;
        }
        else
        {
            StartCoroutine(DecreaseSpeed());
            currentState = walkingState;
        }
    }

    public void switchState(ChickenState state)
    {
        currentState = state;
        state.OnEnterState(this);
    }

    IEnumerator DecreaseSpeed()
    {
        yield return new WaitForSeconds(5);
        agent.speed = initSpeed;
    }

    private void OnDisable()
    {
        if(chickenUI != null)
        {
            chickenUI.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (chickenUI != null)
        {
            chickenUI.SetActive(true);
        }
    }

    public Transform centerPos => centerPosition;
    public float MaxSpeed => maxSpeed;
    public float WalkRange => walkingrange;
    public float WalkDelay => walkDelay;
    public NavMeshAgent navAgent => agent;
    public Collider[] Players => players;
    public GameObject PlayerDetected => playerDetected;

    public bool ChickenIsTaken { get => chickenIsTaken; set => chickenIsTaken = value; }
    public Chicken_WalkingState WalkingState => walkingState;

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
