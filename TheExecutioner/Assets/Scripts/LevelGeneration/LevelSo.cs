using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
[Serializable]
public class LevelSo : ScriptableObject
{
    public int LevelCounter;

    public List<PlatformInformation> StageOne = new List<PlatformInformation>();
    public List<PlatformInformation> StageTwo = new List<PlatformInformation>();
    public List<PlatformInformation> StageThree = new List<PlatformInformation>();
    public int LevelCount;

    public void SaveLevel(List<PlatformInformation> level,int index)
    {
        if (index == 0)
        {
            StageOne = level;
        }

        if (index == 1)
        {
            StageTwo = level;
        }

        if (index == 2)
        {
            StageThree = level;
        }
    }
    
     public List<PlatformInformation> ReturnLevel(int index)
    {
        if (index == 0)
            return StageOne;

        if (index == 1)
            return StageTwo;

        if (index == 2)
            return StageThree;
        
        
        return null;
        
    }
    public void ClearSo()
    {
        StageOne.Clear();
        StageTwo.Clear();
        StageThree.Clear();
    }
}
