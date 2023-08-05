using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Movement_Chicken : MonoBehaviour
{
    [Space]
    [SerializeField] Transform centrePoint;
    [SerializeField] float walkingRange;
    [SerializeField] float timeDelayToWalk = 2f;
    [Header("Player Detected Settings")]
    [SerializeField] float playerRadiusDetection = 5f;
    [SerializeField] float speed = 10f;
    [SerializeField] float acceleration = 10f;
    float initAccel;
    float initSpeed;
    bool isWalking;
    [SerializeField] bool isTaken;
    NavMeshAgent agent;

    Animator anim;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        initAccel = agent.acceleration;
        initSpeed = agent.speed;
    }

    void Update()
    {
        WalkInRandomPos();
        RunAwayIfPlayerIsDetected();
        ChickenAnimation();
    }

    private void RunAwayIfPlayerIsDetected()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerRadiusDetection);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                agent.acceleration = acceleration;
                agent.speed = speed;
                isWalking = false;
                WalkInRandomPos();
            }
            else
            {
                agent.acceleration = initAccel;
                agent.speed = initSpeed;
            }
        }
    }

    void ChickenAnimation()
    {
        if (agent.velocity.magnitude > 0)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Eat", false);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Eat", true);
        }
    }

    void WalkInRandomPos()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !isWalking) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, walkingRange, out point)) //pass in our centre point and radius of area
            {
                isWalking = true;
                Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                StartCoroutine(StartWalking());
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

    IEnumerator StartWalking()
    {
        yield return new WaitForSeconds(timeDelayToWalk);
        isWalking = false;
    }

    public bool ChickenIsTaken { get => isTaken; set => isTaken = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position, playerRadiusDetection);
    }
}