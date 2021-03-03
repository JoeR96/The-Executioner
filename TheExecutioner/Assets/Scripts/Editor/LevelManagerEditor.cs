using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelManager levelManager = (LevelManager)target;
        
        if (GUILayout.Button("Save Level One"))
        {
            levelManager.SaveStageOne();
        }

        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(1);
        }
        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(1);
        }
        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(2);
        }
    }
    
}
