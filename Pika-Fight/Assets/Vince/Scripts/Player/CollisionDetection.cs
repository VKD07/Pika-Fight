using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollisionDetection : MonoBehaviour
{
    GameObject ballDetected;
    GameObject chickenDetected;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!collision.gameObject.GetComponent<Ball>().BallTaken)
            {
                ballDetected = collision.gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chicken")
        {
            chickenDetected = other.gameObject;
        }
    }

    public GameObject BallDetected { get => ballDetected; set => ballDetected = value; }
    public GameObject ChickenDetected { get => chickenDetected; set => chickenDetected = value; }
}