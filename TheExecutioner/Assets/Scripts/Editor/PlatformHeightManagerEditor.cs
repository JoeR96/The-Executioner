using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformHeightManager))]
public class PlatformHeightManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformHeightManager PlatformHeightManager = (PlatformHeightManager)target;
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("1",GUILayout.Width(80), GUILayout.Height(80)))
        { 
            PlatformHeightManager.SetPlatformHeight(0);
        }
        if (GUILayout.Button("2",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(1);
        }
        if (GUILayout.Button("3",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(2);
        }
        
        if (GUILayout.Button("4",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(3);
        }
        if (GUILayout.Button("5",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(4);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("6",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(5);
        }

        if (GUILayout.Button("7",GUILayout.Width(80), GUILayout.Height(80)))
        { PlatformHeightManager.SetPlatformHeight(6);
        }
        if (GUILayout.Button("8",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(7);
        }
        if (GUILayout.Button("9",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(8);
        }
 
        if (GUILayout.Button("Level 10",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(9);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("11",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(10);
        }
        if (GUILayout.Button("12",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(11);
        }

        if (GUILayout.Button("13",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(12);
        }
        if (GUILayout.Button("14",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(13);
        }
        if (GUILayout.Button("Reset Platform",GUILayout.Width(80), GUILayout.Height(80)))
        {
            PlatformHeightManager.SetPlatformHeight(16);
        }
        EditorGUILayout.EndHorizontal();
    }
}
