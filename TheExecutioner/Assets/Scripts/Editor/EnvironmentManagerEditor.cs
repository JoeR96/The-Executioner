using System.Collections;
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
        if (GUILayout.Button("Create Lower Path"))
        {
            environmentManager.StartBunkers();
        }
        if (GUILayout.Button("Create Low Path"))
        {

            environmentManager.StartHighBunkers();

        }
        if (GUILayout.Button("Create Above Path"))
        {

            environmentManager.StartLowBunkers();

        }
        
        if (GUILayout.Button("Reset Platforms"))
        {
            environmentManager.LowerAll();
        }

        if (GUILayout.Button("Lower Last Platform"))
        {
            
        }
        if (GUILayout.Button("BuildNavmesh"))
        {
            environmentManager.BuildNavMesh();
        }

    }
}
