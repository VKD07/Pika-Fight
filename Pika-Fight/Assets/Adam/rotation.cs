using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public float  rotationSpeed= 5f;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Rotate(0, 10, 0, Space.Self);
    }
}
