using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingHandles : MonoBehaviour
{
   public List<Vector3> points = new List<Vector3>();


    public void MovePoint(int i, Vector3 pos)
    {
        points[i] = pos;
    }
}
