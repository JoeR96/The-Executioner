using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformManager))]
public class PlatformManagerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformManager PlatformManager = (PlatformManager)target;
       
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Toggle Spawn Point",GUILayout.Width(100), GUILayout.Height(100)))
        {
            PlatformManager.PlatformSpawnManager.ReturnPlatformSpawnPointValue();
        }
        if (GUILayout.Button("Toggle Event Spawn Point",GUILayout.Width(100), GUILayout.Height(100))) { 
            PlatformManager.PlatformSpawnManager.ReturnPlatformEventSpawnPointValue();
        }
        EditorGUILayout.EndHorizontal ();
            
        EditorGUILayout.BeginHorizontal ();
        if (PlatformManager.PlatformColourManager.ColourTileMode)
        {
            if (GUILayout.Button("Set tile Red",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformColourManager.SetPlatformColour(0);

            }
            if (GUILayout.Button("Set tile Blue",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformColourManager.SetPlatformColour(1);

            }
            if (GUILayout.Button("Set tile Green",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformColourManager.SetPlatformColour(2);

            }
            if (GUILayout.Button("Set tile Orange",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformColourManager.SetPlatformColour(3);

            }
            if (GUILayout.Button("Set tile White",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformColourManager.SetPlatformColour(4);
 
            }
        
            
        }
        EditorGUILayout.EndHorizontal ();
      
    }
}
