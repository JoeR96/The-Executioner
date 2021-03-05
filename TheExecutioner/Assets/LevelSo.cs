using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[Serializable]
public class LevelSo : ScriptableObject
{
    
    public List<PlatformInformation[,]> Levels = new List<PlatformInformation[,]>();

    public int LevelCount;
    
    private void OnEnable()
    {
        LevelCount = Levels.Count;
    }

    
    public void AddLevel(PlatformInformation[,] level)
    {
        Levels.Add(level);
        LevelCount = Levels.Count;
    }
}
