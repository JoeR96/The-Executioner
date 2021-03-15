using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformBridgeManager))]
public class PlatformBridgeManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformBridgeManager PlatformBridgeManager = (PlatformBridgeManager)target;
        
        EditorGUILayout.BeginHorizontal ();
        if (PlatformBridgeManager.PlatformBridgeActive)
        {
            if (GUILayout.Button("Set Low Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformBridgeManager.SetBridgeHeight(0);
            }
            if (GUILayout.Button("Set Medium Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformBridgeManager.SetBridgeHeight(1);
            }
            if (GUILayout.Button("Set High Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformBridgeManager.SetBridgeHeight(2);
            }
        }
        EditorGUILayout.EndHorizontal ();
    }
}
