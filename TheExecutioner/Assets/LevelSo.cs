using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class LevelSo : ScriptableObject
{
    public List<PlatformState[,]> Levels = new List<PlatformState[,]>();
    public Node[,] Level;
    public int LevelCount;

    private void Start()
    {
        
    }

    public void AddLevel(PlatformState[,] level)
    {
        Levels.Add(level);
        LevelCount = Levels.Count;
    }
}
