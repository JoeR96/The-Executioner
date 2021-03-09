using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformState))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PlatformState platformState = (PlatformState)target;
        EditorGUILayout.BeginHorizontal ();
        if (!GameManager.instance.EnvironmentManager.InversePlatforms)
        {
            if (GUILayout.Button("Create Low Path",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetPlatformHeight((int)PlatformHeight.Raised);
            }
            if (GUILayout.Button("Create High Path",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
            }
            if (GUILayout.Button("Lower Platform Underground",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetPlatformHeight((int)PlatformHeight.Underground);
            }
            if (GUILayout.Button("Reset Platform",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetPlatformHeight((int)PlatformHeight.Flat);
            }
        }
        else
        {
            if (GUILayout.Button("Create Low Path",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetNegativePlatformHeight((int)PlatformHeight.Raised);
            }
            if (GUILayout.Button("Create High Path",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetNegativePlatformHeight((int)PlatformHeight.RaisedTwice);
            }
            if (GUILayout.Button("Lower Platform Underground",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetNegativePlatformHeight((int)PlatformHeight.Underground);
            }
            if (GUILayout.Button("Reset Platform",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetNegativePlatformHeight((int)PlatformHeight.Flat);
            }
            
        }
        if (GUILayout.Button("Toggle Stair",GUILayout.Width(125), GUILayout.Height(125)))
        {
            platformState.ActivateStairs(platformState.ReturnStairValue());
        }
        EditorGUILayout.EndHorizontal ();

        
        EditorGUILayout.BeginHorizontal ();
        if (platformState.PlatformStairActive)
        {
            if (GUILayout.Button("Stair Position One",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetStairRotation(0);
            }
            if (GUILayout.Button("Stair Position Two",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetStairRotation(1);
            }
            if (GUILayout.Button("Stair Position Three",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetStairRotation(2);
            }
            if (GUILayout.Button("Stair Position Four",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetStairRotation(3);
            }
        }
        EditorGUILayout.EndHorizontal ();
        if (GUILayout.Button("Toggle Bridge",GUILayout.Width(125), GUILayout.Height(125)))
        {
            platformState.ActivateBridge(platformState.ReturnBridgeValue());
        }
        EditorGUILayout.BeginHorizontal ();
        if (platformState.PlatformBridgeActive)
        {
            if (GUILayout.Button("Set Low Bridge",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetBridgeHeight((int)PlatformHeight.Raised);
            }
            if (GUILayout.Button("Set Medium Bridge",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetBridgeHeight((int)PlatformHeight.RaisedTwice);
            }
            if (GUILayout.Button("Set High Bridge",GUILayout.Width(125), GUILayout.Height(125)))
            {
                platformState.SetBridgeHeight((int)PlatformHeight.Underground);
            }
        }
        EditorGUILayout.EndHorizontal ();

    }
}
