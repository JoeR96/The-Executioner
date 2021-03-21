using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;
[CustomEditor(typeof(EnvironmentManager))]
public class EnvironmentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvironmentManager environmentManager = (EnvironmentManager)target;
        
        
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Reset Path",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.LowerAll();
        }
        if (GUILayout.Button("Raise Wall",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.RaiseWall(true);
        }
        if (GUILayout.Button("Lower Wall",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.RaiseWall(false);
        }
        EditorGUILayout.EndHorizontal ();
    }
}
