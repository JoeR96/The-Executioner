using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaterformStairManagerEditor : Editor
{
    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformRampManager platformRampManager = (PlatformRampManager)target;
        
        EditorGUILayout.BeginHorizontal ();
        
        
        if (platformRampManager.PlatformRampActive)
        {
            if (GUILayout.Button("One",GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(0);
            }
            if (GUILayout.Button("Two",GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(1);
            }
            if (GUILayout.Button("Three",GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(2);
            }
            if (GUILayout.Button("Four",GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(3);
            }
            if (GUILayout.Button("Stair",GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.ActivateRamp(platformRampManager.ReturnRampValue());
            }
        }
        EditorGUILayout.EndHorizontal ();

    }
}