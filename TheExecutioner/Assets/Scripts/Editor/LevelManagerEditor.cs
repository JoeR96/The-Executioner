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
        
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelManager levelManager = (LevelManager)target;
        levelSo = levelManager.levelSo;
        if (levelManager.BuildMode)
        {
            if (GUILayout.Button("Save Level One",GUILayout.Width(250), GUILayout.Height(50)))
            {
                levelManager.SaveToList(0);
                EditorUtility.SetDirty(levelSo);
            }
            if (GUILayout.Button("Save Level Two",GUILayout.Width(250), GUILayout.Height(50)))
            {
                levelManager.SaveToList(1);
                EditorUtility.SetDirty(levelSo);
            }
            if (GUILayout.Button("Save Level Three",GUILayout.Width(250), GUILayout.Height(50)))
            {
                levelManager.SaveToList(2);
                EditorUtility.SetDirty(levelSo);
            }
        }
      
        
        if (GUILayout.Button("Load Layout One",GUILayout.Width(250), GUILayout.Height(50)))
        {
            levelManager.LoadStage(0);
        }
        if (GUILayout.Button("Load Layout Two",GUILayout.Width(250), GUILayout.Height(50)))
        {
            levelManager.LoadStage(1);
        }
        if (GUILayout.Button("Load Layout Three",GUILayout.Width(250), GUILayout.Height(50)))
        {
            levelManager.LoadStage(2);
        }
        
    }
    
}
