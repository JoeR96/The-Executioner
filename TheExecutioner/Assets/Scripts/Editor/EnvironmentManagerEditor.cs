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
        if (GUILayout.Button("Create Low Path"))
        {
            environmentManager.StartBunkers();
        }
        if (GUILayout.Button("Create High Path"))
        {

            environmentManager.StartHighBunkers();

        }
        if (GUILayout.Button("Create Underground Path"))
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
        if (GUILayout.Button("Smooth Platforms"))
        {
            environmentManager.BuildNavMesh();
        }

        if (GUILayout.Button("Raise Walls"))
        {
            environmentManager.RaiseWall();
        }
    }
}
