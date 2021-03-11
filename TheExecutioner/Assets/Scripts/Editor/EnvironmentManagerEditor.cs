﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(EnvironmentManager))]
public class EnvironmentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvironmentManager environmentManager = (EnvironmentManager)target;

        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Create Lower Path",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.StartBunkers();
        }
        if (GUILayout.Button("Create Low Path",GUILayout.Width(125), GUILayout.Height(125)))
        {

            environmentManager.StartHighBunkers();

        }
        if (GUILayout.Button("Create High Path",GUILayout.Width(125), GUILayout.Height(125)))
        {

            environmentManager.StartLowBunkers();

        }
        EditorGUILayout.EndHorizontal ();
        
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Reset Path",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.LowerAll();
        }
        if (GUILayout.Button("Raise Wall",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.RaiseWall();
        }
        if (GUILayout.Button("Raise wall Two",GUILayout.Width(125), GUILayout.Height(125)))
        {
            environmentManager.RaiseWallTwo();
        }
        EditorGUILayout.EndHorizontal ();
    }
}