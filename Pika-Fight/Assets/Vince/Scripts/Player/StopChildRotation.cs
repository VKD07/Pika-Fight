using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopChildRotation : MonoBehaviour
{
    [SerializeField] Transform child;

    private void Update()
    {
      //  child.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
