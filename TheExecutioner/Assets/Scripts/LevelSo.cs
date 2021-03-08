using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[Serializable]
public class LevelSo : ScriptableObject
{
    
    public List<PlatformInformation> LevelOne = new List<PlatformInformation>();
    public List<PlatformInformation> LevelTwo = new List<PlatformInformation>();
    public List<PlatformInformation> LevelThree = new List<PlatformInformation>();
    public int LevelCount;
    


    
    public void AddLevel(List<PlatformInformation> level,int index)
    {
        if (index == 0)
        {
            LevelOne = level;
        }

        if (index == 1)
        {
            LevelTwo = level;
        }

        if (index == 2)
        {
            LevelThree = level;
        }
    }

    public void ClearSo()
    {
        LevelOne.Clear();
        LevelTwo.Clear();
        LevelThree.Clear();
    }

    public List<PlatformInformation> ReturnLevel(int index)
    {
        if (index == 0)
            return LevelOne;

        if (index == 1)
            return LevelTwo;

        if (index == 2)
            return LevelThree;
        
        
        return null;
        
    }
}
