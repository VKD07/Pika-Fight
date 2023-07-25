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
        LoadSceneButton("Player Join Scene", "PlayerJoin");
        LoadSceneButton("Normal Map Test", "VinceTest");
        LoadSceneButton("Score Scene", "ScoreScene");
        EditorGUILayout.EndHorizontal();
        LoadAdamSceneButton("Adam Scene", "adam");
    }

    void LoadSceneButton(string btnName, string SceneName)
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
