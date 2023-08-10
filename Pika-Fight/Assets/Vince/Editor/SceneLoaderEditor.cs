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
        LoadAdamSceneButton("Adam Scene", "AdamMap");
        LoadAlyScenes("Moving Land", "ShrinkLevel1");
        LoadAlyScenes("Fiery Land", "ExplosionLevel");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        LoadVinceScenes("Final Ranking", "FinalRanking");
        LoadVinceScenes("Score Scene", "ScoreScene");
        LoadVinceScenes("Random Scene", "RandomizationScene");
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

    void LoadAlyScenes(string btnName, string SceneName)
    {
        if (GUILayout.Button(btnName))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Ali/Scenes/" + SceneName + ".unity");
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
