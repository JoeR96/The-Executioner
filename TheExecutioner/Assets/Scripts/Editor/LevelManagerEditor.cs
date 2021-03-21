using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public int currentLevel;
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

            EditorGUILayout.BeginHorizontal ();
            if (GUILayout.Button("Save Stage One",GUILayout.Width(125), GUILayout.Height(125)))
            {
                levelManager.SaveStage(0);
                EditorUtility.SetDirty(levelSo);
            }
            if (GUILayout.Button("Save Stage Two",GUILayout.Width(125), GUILayout.Height(125)))
            {
                levelManager.SaveStage(1);
                EditorUtility.SetDirty(levelSo);
            }
            if (GUILayout.Button("Save Stage Three",GUILayout.Width(125), GUILayout.Height(125)))
            {
                levelManager.SaveStage(2);
                EditorUtility.SetDirty(levelSo);
            }
            if (GUILayout.Button("CLEAR THE LIIIIST NEVERR",GUILayout.Width(125), GUILayout.Height(125)))
            {
                levelSo.ClearSo();
            
            }
            EditorGUILayout.EndHorizontal ();
        }
        
        EditorGUILayout.BeginHorizontal ();
        if (GUILayout.Button("Load Stage One",GUILayout.Width(125), GUILayout.Height(125)))
        {
            levelManager.LoadStage(0);
            EditorUtility.SetDirty(levelSo);
        }
        if (GUILayout.Button("Load Stage Two",GUILayout.Width(125), GUILayout.Height(125)))
        {
            levelManager.LoadStage(1);
            EditorUtility.SetDirty(levelSo);
        }
        if (GUILayout.Button("Load Stage Three",GUILayout.Width(125), GUILayout.Height(125)))
        {
            levelManager.LoadStage(2);
            EditorUtility.SetDirty(levelSo);
        }
       
        EditorGUILayout.EndHorizontal ();
   
    }
    
}
