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
        if (GUILayout.Button("Level 1", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(0);
        }

        if (GUILayout.Button("Level 2", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(1);
        }

        if (GUILayout.Button("Level 3", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(2);
        }

        if (GUILayout.Button("Level 4", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(3);
        }

        if (GUILayout.Button("Level 5", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(4);
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Level 6", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(5);
        }
        
        if (GUILayout.Button("Level 7", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(6);
        }
        
        if (GUILayout.Button("Level 8", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(7);
        }
        
        if (GUILayout.Button("Level 9", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(8);
        }
        
        if (GUILayout.Button("Level 10", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(9);
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Level 11", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(10);
        }
        
        if (GUILayout.Button("Level 12", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(11);
        }
        
        if (GUILayout.Button("Level 13", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(12);
        }
        
        if (GUILayout.Button("Level 14", GUILayout.Width(80), GUILayout.Height(80)))
        {
            environmentSpawner.SpawnPath(13);
        }

        EditorGUILayout.EndHorizontal();
    }


}
