using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class SceneLoaderEditor : EditorWindow
{
    [MenuItem("Tools/Scene Loader Tool")] // Add a valid menu path to make it visible.
    public static void ShowWindow()
    {
        GetWindow<SceneLoaderEditor>("Scene Loader");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select A Scene To Go", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        LoadVinceScenes("Player Join Scene", "PlayerJoin");
        LoadVinceScenes("Normal Map Test", "VinceTest");
        LoadVinceScenes("Score Scene", "ScoreScene");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        LoadAdamSceneButton("Adam Scene", "adam");
        LoadVinceScenes("Final Ranking", "FinalRanking");
        EditorGUILayout.EndHorizontal();
    }

    void LoadVinceScenes(string btnName, string SceneName)
    {
        if (GUILayout.Button(btnName))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Vince/Scenes/" + SceneName + ".unity");
        }
    }

    void LoadAdamSceneButton(string btnName, string SceneName)
    {
        if (GUILayout.Button(btnName))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Adam/" + SceneName + ".unity");
        }
    }
}
