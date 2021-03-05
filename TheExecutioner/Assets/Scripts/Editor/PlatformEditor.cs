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
        if (GUILayout.Button("Create Low Path"))
        {
            platformState.SetPlatformHeight((int)PlatformHeight.Raised);
        }
        if (GUILayout.Button("Create High Path"))
        {
            platformState.SetPlatformHeight((int)PlatformHeight.RaisedTwice);
        }
        if (GUILayout.Button("Lower Platform Underground"))
        {
            platformState.SetPlatformHeight((int)PlatformHeight.Underground);
        }
        if (GUILayout.Button("Reset Platform"))
        {
            platformState.SetPlatformHeight((int)PlatformHeight.Flat);
        }

        if (GUILayout.Button("Toggle Stair"))
        {
            platformState.ActivateStairs(platformState.ReturnStairValue());
        }
        if (GUILayout.Button("Stair Position One"))
        {
            platformState.SetStairRotation(0);
        }
        if (GUILayout.Button("Stair Position Two"))
        {
            platformState.SetStairRotation(1);
        }
        if (GUILayout.Button("Stair Position Three"))
        {
            platformState.SetStairRotation(2);
        }
        if (GUILayout.Button("Stair Position Four"))
        {
            platformState.SetStairRotation(3);
        }
        if (GUILayout.Button("Toggle Bridge"))
        {
            platformState.ActivateBridge(platformState.ReturnBridgeValue());
        }
        if (GUILayout.Button("Set Low Bridge"))
        {
            platformState.SetBridgeHeight((int)PlatformHeight.Raised);
        }
        if (GUILayout.Button("Set Medium Bridge"))
        {
            platformState.SetBridgeHeight((int)PlatformHeight.RaisedTwice);
        }
        if (GUILayout.Button("Set High Bridge"))
        {
            platformState.SetBridgeHeight((int)PlatformHeight.Underground);
        }
   

    }
}
