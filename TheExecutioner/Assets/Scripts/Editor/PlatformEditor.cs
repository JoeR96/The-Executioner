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
            platformState.SetPlatformHeight(PlatformHeight.Raised);
        }
        if (GUILayout.Button("Create High Path"))
        {
            platformState.SetPlatformHeight(PlatformHeight.RaisedTwice);
        }
        if (GUILayout.Button("Lower Platform Underground"))
        {
            platformState.SetPlatformHeight(PlatformHeight.Underground);
        }
        if (GUILayout.Button("Reset Platform"))
        {
            platformState.SetPlatformHeight(PlatformHeight.Flat);
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
            platformState.SetStairRotation((PlatformStairState) 1);
        }
        if (GUILayout.Button("Stair Position Three"))
        {
            platformState.SetStairRotation((PlatformStairState) 2);
        }
        if (GUILayout.Button("Stair Position Four"))
        {
            platformState.SetStairRotation((PlatformStairState) 3);
        }
    }
}
