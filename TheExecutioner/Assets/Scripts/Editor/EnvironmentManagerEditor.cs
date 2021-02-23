using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(EnvironmentManager))]
public class EnvironmentManagerEditor : Editor
{
    public override void OnInspectorGUI() //2
    {
        base.DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        // Call base class method
        
        // Custom form for Player Preferences
        EnvironmentManager  environmentManager = (EnvironmentManager)target;
        

        if (GUILayout.Button("Spawn Paths")) //8
        {
            environmentManager.pathFinding.SpawnPaths();
        }

        if (GUILayout.Button("Raise Paths")) //10
        {
            
            
        }
        if (GUILayout.Button("Clear Paths")) //10
        {
            environmentManager.ClearPaths();
            
        }
        if (GUILayout.Button("Spawn stairs")) //10
        {
            environmentManager.SpawnStairs();
            
        }
        if (GUILayout.Button("Lower Paths")) //10
        {
            
            environmentManager.LowerAllPathPlatforms();
        }
        
        if (GUILayout.Button("Raise Random")) //10
        {
            //ObjectPooler.Instance.ReturnObject();
        }
        
        if (GUILayout.Button("Reset Random")) //10
        {
            //ObjectPooler.Instance.ReturnObject();
        }

        GUILayout.EndHorizontal();
        
        // Custom Button with Image as Thumbnail
    }
}
