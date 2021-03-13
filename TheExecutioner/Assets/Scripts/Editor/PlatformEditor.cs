using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformManager))]
public class PlatformManagerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformManager PlatformManager = (PlatformManager)target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Level 1",GUILayout.Width(100), GUILayout.Height(100)))
        { PlatformManager.PlatformHeightManager.SetPlatformHeight(0);
            }
            if (GUILayout.Button("Level 2",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight(1);
            }
            if (GUILayout.Button("Level 3",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight(2);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal ();
            if (GUILayout.Button("Level 4",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight(3);
            }
            if (GUILayout.Button("Level 5",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight(4);
            }
            if (GUILayout.Button("Level 6",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight(5);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal ();
        
            if (GUILayout.Button("Toggle Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformBridgeManager.ActivateBridge(PlatformManager.PlatformBridgeManager.ReturnBridgeValue());
            }
            if (GUILayout.Button("Toggle Stair",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformRampManager.ActivateRamp(PlatformManager.PlatformRampManager.ReturnRampValue());
            }
            if (GUILayout.Button("Toggle Spawn Point",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformSpawnManager.ReturnPlatformSpawnPointValue();
            }
            if (GUILayout.Button("Reset Platform",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformHeightManager.SetPlatformHeight((int)PlatformHeight.Flat);
            }
            EditorGUILayout.EndHorizontal ();

        
        EditorGUILayout.BeginHorizontal ();
        if (PlatformManager.PlatformRampManager.PlatformRampActive)
        {
            if (GUILayout.Button("Stair Position One",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(0);
            }
            if (GUILayout.Button("Stair Position Two",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(1);
            }
            if (GUILayout.Button("Stair Position Three",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(2);
            }
            if (GUILayout.Button("Stair Position Four",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformRampManager.SetRampRotation(3);
            }
        }
        EditorGUILayout.EndHorizontal ();
        
        EditorGUILayout.BeginHorizontal ();
        if (PlatformManager.PlatformBridgeManager.PlatformBridgeActive)
        {
            if (GUILayout.Button("Set Low Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(0);
            }
            if (GUILayout.Button("Set Medium Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(1);
            }
            if (GUILayout.Button("Set High Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                PlatformManager.PlatformBridgeManager.SetBridgeHeight(2);
            }
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
        EditorGUILayout.BeginHorizontal ();
        if (PlatformManager.PlatformColourManager.ColourAdjacentMode)
        {

            
            if (GUILayout.Button("Set Adjacent Red",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(5);
            
            }
            if (GUILayout.Button("Set Adjacent Blue",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(6);
             
            }
            if (GUILayout.Button("Set Adjacent Green",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(7);
                
            }
            if (GUILayout.Button("Set Adjacent Orange",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(8);
                
            }
            if (GUILayout.Button("Set Adjacent White",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(9);
                
            }
            if (GUILayout.Button("Set Adjacent Black",GUILayout.Width(125), GUILayout.Height(125)))
            {
                PlatformManager.PlatformColourManager.SetAdjacentColour(10);
                
            }
            
        }
        EditorGUILayout.EndHorizontal ();
    }
}
