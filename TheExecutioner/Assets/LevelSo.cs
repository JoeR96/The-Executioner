using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class LevelSo : ScriptableObject
{
    public List<Node[,]> Levels = new List<Node[,]>();
    public Node[,] Level;
    public int LevelCount;

    private void Start()
    {
        LevelCount = Levels.Count;
    }
}
