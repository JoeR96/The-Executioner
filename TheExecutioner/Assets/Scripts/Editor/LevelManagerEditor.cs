using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    [SerializeField] private LevelSo levelSo;
    private void Start()
    {
        levelSo = (LevelSo)target;
        EditorUtility.SetDirty(levelSo);
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelManager levelManager = (LevelManager)target;
        
        if (GUILayout.Button("Save "))
        {
            levelManager.SaveStageOne();
            if (GUI.changed) EditorUtility.SetDirty(levelSo);
        }

        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(0);
        }
        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(1);
        }
        if (GUILayout.Button("Load Layout One"))
        {
            levelManager.LoadStage(2);
        }

        if (GUILayout.Button("Clear Levels"))
        {
            levelManager.ClearSo();
            
        }
    }
    
}
