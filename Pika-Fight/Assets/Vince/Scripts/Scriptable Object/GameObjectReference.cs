using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectReference", menuName = "References/GameObjectReference")]
public class GameObjectReference : ScriptableObject
{
    [SerializeField] GameObject objectDetected;
    public GameObject ObjectDetected { get =>  objectDetected; set { objectDetected = value;} }
}
