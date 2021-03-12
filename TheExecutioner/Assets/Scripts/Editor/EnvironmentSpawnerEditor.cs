using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnvironmentSpawner))]
public class EnvironmentSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvironmentSpawner environmentSpawner = (EnvironmentSpawner) target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Level 1", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(0);
        }

        if (GUILayout.Button("Level 2", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(1);
        }

        if (GUILayout.Button("Level 3", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(2);
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Level 4", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(3);
        }

        if (GUILayout.Button("Level 5", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(4);
        }

        if (GUILayout.Button("Level 6", GUILayout.Width(100), GUILayout.Height(100)))
        {
            environmentSpawner.SpawnPath(5);
        }
        EditorGUILayout.EndHorizontal();
    }


}
