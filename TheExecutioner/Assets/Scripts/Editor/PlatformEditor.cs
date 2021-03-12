using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlatformState))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformState platformState = (PlatformState)target;
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Level 1",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(1);
            }
            if (GUILayout.Button("Level 2",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(2);
            }
            if (GUILayout.Button("Level 3",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(3);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal ();
            if (GUILayout.Button("Level 4",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(4);
            }
            if (GUILayout.Button("Level 5",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(5);
            }
            if (GUILayout.Button("Level 6",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight(6);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal ();
        
            if (GUILayout.Button("Toggle Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.ActivateBridge(platformState.ReturnBridgeValue());
            }
            if (GUILayout.Button("Toggle Stair",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.ActivateStairs(platformState.ReturnStairValue());
            }
            if (GUILayout.Button("Toggle Spawn Point",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.ReturnPlatformSpawnPointValue();
            }
            if (GUILayout.Button("Reset Platform",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformHeight((int)PlatformHeight.Flat);
            }
            EditorGUILayout.EndHorizontal ();

        
        EditorGUILayout.BeginHorizontal ();
        if (platformState.PlatformStairActive)
        {
            if (GUILayout.Button("Stair Position One",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetStairRotation(0);
            }
            if (GUILayout.Button("Stair Position Two",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetStairRotation(1);
            }
            if (GUILayout.Button("Stair Position Three",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetStairRotation(2);
            }
            if (GUILayout.Button("Stair Position Four",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetStairRotation(3);
            }
        }
        EditorGUILayout.EndHorizontal ();
        
        EditorGUILayout.BeginHorizontal ();
        if (platformState.PlatformBridgeActive)
        {
            if (GUILayout.Button("Set Low Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetBridgeHeight(0);
            }
            if (GUILayout.Button("Set Medium Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetBridgeHeight(1);
            }
            if (GUILayout.Button("Set High Bridge",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetBridgeHeight(2);
            }
        }
        EditorGUILayout.EndHorizontal ();
        
        EditorGUILayout.BeginHorizontal ();
        if (platformState.ColourTileMode)
        {
            if (GUILayout.Button("Set tile Red",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformColour(0);

            }
            if (GUILayout.Button("Set tile Blue",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformColour(1);

            }
            if (GUILayout.Button("Set tile Green",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformColour(2);

            }
            if (GUILayout.Button("Set tile Orange",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformColour(3);

            }
            if (GUILayout.Button("Set tile White",GUILayout.Width(100), GUILayout.Height(100)))
            {
                platformState.SetPlatformColour(4);
 
            }
        
            
        }
        EditorGUILayout.EndHorizontal ();
        EditorGUILayout.BeginHorizontal ();
        if (platformState.ColourAdjacentMode)
        {

            
            if (GUILayout.Button("Set Adjacent Red",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(5);
            
            }
            if (GUILayout.Button("Set Adjacent Blue",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(6);
             
            }
            if (GUILayout.Button("Set Adjacent Green",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(7);
                
            }
            if (GUILayout.Button("Set Adjacent Orange",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(8);
                
            }
            if (GUILayout.Button("Set Adjacent White",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(9);
                
            }
            if (GUILayout.Button("Set Adjacent Black",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetAdjacentColour(10);
                
            }
            
        }
        EditorGUILayout.EndHorizontal ();
    }
}
