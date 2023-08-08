using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeBoids : MonoBehaviour
{
    CustomLinkedList<BeeBoids> neighbours;
    Vector3 velocity;
    [SerializeField] float seperationForce;
    [SerializeField] float alignmentForce;
    [SerializeField] float cohesionForce;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float attractionStrength;
    [SerializeField] public Transform targetObject;
    void Start()
    {
        velocity = Random.insideUnitSphere * 10;
        neighbours = new CustomLinkedList<BeeBoids>();
    }

    void Update()
    {
        Seperation();
        Alignment();
        Cohesion();
        transform.position += velocity * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    void Seperation()
    {
        Vector3 seperationVector = new Vector3();
        foreach(var bees in neighbours)
        {
            var direction = transform.position - bees.transform.position;
            seperationVector += direction;
        }
        seperationVector.Normalize();
        velocity += seperationVector * seperationForce;
    }

    void Alignment()
    {
        Vector3 alignmentVector = new Vector3();
        foreach (var bees in neighbours)
        {
            var direction = bees.velocity;
            alignmentVector += direction;
        }
        alignmentVector.Normalize();
        velocity += alignmentVector * alignmentForce;
    }

    void Cohesion()
    {
        Vector3 cohesionPoint = Vector3.zero;
        foreach (var bees in neighbours)
        {
            cohesionPoint += bees.transform.position;
            bees.transform.LookAt(targetObject);
        }
        cohesionPoint /= neighbours.count;

        Vector3 targetDirection = targetObject.transform.position - transform.position;
        cohesionPoint += targetDirection * attractionStrength;

        var directionVector = cohesionPoint - transform.position;
        directionVector.Normalize();
        velocity += directionVector * cohesionForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        var boid = other.GetComponent<BeeBoids>();
        if (boid != null)
        {
            print("Bee detected");
            Node<BeeBoids> newBoid = new Node<BeeBoids>(boid);
            neighbours.AddFirst(newBoid);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var boid = other.GetComponent<BeeBoids>();
        if (boid != null)
        {
            Node<BeeBoids> nodeToRemove = neighbours.Find(boid);
            if (nodeToRemove != null)
            {
                neighbours.Remove(nodeToRemove);
            }
        }
    }
}
