using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(PlatformRampManager))]
public class PlatformRampManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();
        PlatformRampManager platformRampManager = (PlatformRampManager) target;
        if (GUILayout.Button("Toggle Ramo", GUILayout.Width(80), GUILayout.Height(80)))
        {
            platformRampManager.ActivateRamp(platformRampManager.ReturnRampValue());
        }
        if (platformRampManager.PlatformRampActive)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("One", GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(0);
            }

            if (GUILayout.Button("Two", GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(1);
            }

            if (GUILayout.Button("Three", GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(2);
            }

            if (GUILayout.Button("Four", GUILayout.Width(80), GUILayout.Height(80)))
            {
                platformRampManager.SetRampRotation(3);
            }

           

            EditorGUILayout.EndHorizontal();
        }
    }
}
