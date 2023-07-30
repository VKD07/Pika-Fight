using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpChicken : MonoBehaviour
{
    [SerializeField] GameObject chicken;
    [SerializeField] FloatReference playerVelocity;
    [SerializeField] FloatReference playerHealth;
    [SerializeField] Transform chickenPos;
    // Update is called once per frame

    private void OnEnable()
    {
        chicken = null;
    }

    void Update()
    {
        PickUpAndHold();
    }

    private void PickUpAndHold()
    {
        if (chicken != null)
        {
            chicken.transform.position = chickenPos.position;
            chicken.transform.forward = chickenPos.forward;
            chicken.GetComponent<Movement_Chicken>().enabled = false;
            chicken.GetComponent<Movement_Chicken>().ChickenIsTaken = true;
            chicken.GetComponent<Animator>().SetBool("Walk", false);
            if (playerVelocity.Value > 0)
            {
                chicken.GetComponent<Animator>().SetBool("Flap", true);
                chicken.GetComponent<Animator>().SetBool("Eat", false);
            }
            else
            {
                chicken.GetComponent<Animator>().SetBool("Flap", false);
            }

        }
    }

    private void OnDisable()
    {
        DropChicken();
    }

    public void DropChicken()
    {
        chicken.GetComponent<Movement_Chicken>().ChickenIsTaken = false;
        chicken.GetComponent<Movement_Chicken>().enabled = true;
        chicken.GetComponent<Animator>().SetBool("Flap", false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Chicken")
        {
            if (!collision.GetComponent<Movement_Chicken>().ChickenIsTaken)
            {
                chicken = collision.gameObject;
            }
        }
    }

    public FloatReference PlayerVelocity { set => playerVelocity = value; }
    public FloatReference PlayerHealth { set => playerHealth = value; }
}
