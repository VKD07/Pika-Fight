using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(BallSpawner))]
public class BallSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BallSpawner ballSpawner = (BallSpawner)target;
        base.OnInspectorGUI();
        SpawnABall(ballSpawner);
        RemoveAllBalls(ballSpawner);
    }

    private static void SpawnABall(BallSpawner ballSpawner)
    {
        if (GUILayout.Button("Spawn Ball"))
        {
            GameObject ball = Instantiate(ballSpawner.GetBall, ballSpawner.GetSpawnPoint.position, Quaternion.identity);
            ballSpawner.AddToList(ball);
        }
    }

    void RemoveAllBalls(BallSpawner ballSpawner)
    {
        if (GUILayout.Button("Remove All Balls"))
        {
            ballSpawner.ClearList();
        }
    }
}
