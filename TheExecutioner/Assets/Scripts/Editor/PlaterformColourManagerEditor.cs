using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

public class PlaterformColourManagerEditor : Editor
{
    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformColourManager PlatformColourManager = (PlatformColourManager)target;
        
        if (PlatformColourManager.ColourAdjacentMode)
        {
            EditorGUILayout.BeginHorizontal ();
            if (GUILayout.Button("Set Adjacent Red",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(0);
            
            }
            if (GUILayout.Button("Set Adjacent Blue",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(1);
             
            }
            if (GUILayout.Button("Set Adjacent Green",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(2);
                
            }EditorGUILayout.EndHorizontal ();
            EditorGUILayout.BeginHorizontal ();
            
            if (GUILayout.Button("Set Adjacent Orange",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(3);
                
            }
            if (GUILayout.Button("Set Adjacent White",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(4);
                
            }
            if (GUILayout.Button("Set Adjacent Black",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformColourManager.SetAdjacentColour(5);
            }
            EditorGUILayout.EndHorizontal ();
        }
        
    }
}
