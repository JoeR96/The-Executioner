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
        if (GUILayout.Button("Create Path"))
        {
            for (int i = 0; i < Random.Range(6,12); i++)
            {
                environmentManager.StartBunkers();
            }
            
        }
        if (GUILayout.Button("Reset Platforms"))
        {
            environmentManager.LowerAll();
        }

        if (GUILayout.Button("Lower Last Platform"))
        {
            environmentManager.LowerLastPlatform();
        }
        if (GUILayout.Button("Smooth Platforms"))
        {
            environmentManager.SmoothMap();
        }
    }
}
