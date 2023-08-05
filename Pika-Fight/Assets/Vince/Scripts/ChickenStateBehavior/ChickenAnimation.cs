using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenAnimation : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
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
}
