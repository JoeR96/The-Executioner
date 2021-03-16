using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.Searcher;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel;
    public int CurrentStage;
    public List<List<PlatformInformation>> CurrentLevelList = new List<List<PlatformInformation>>();
    public bool BuildMode;
    private List<PlatformStateManager> spawnPoints = new List<PlatformStateManager>();
    public LevelSo levelSo;
    private Grid grid;
  
    void Start()
    {
        grid = GetComponent<Grid>();
    }
    
    

    public PlatformInformation[,] SaveStageInformation()
    {
        PlatformInformation[,] platformStates = new PlatformInformation[grid.gridSizeX, grid.gridSizeY];
        
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            for (int j = 0; j < grid.gridSizeY; j++)
            {

                platformStates[i, j] = new PlatformInformation();
                platformStates[i, j].CurrentHeight = grid.grid[i, j].PlatformManager.PlatformHeightManager.CurrentHeight;
                platformStates[i, j].CurrentRotation = grid.grid[i, j].PlatformManager.PlatformRampManager.CurrentRotation;
                platformStates[i, j].PlatformStairActive = grid.grid[i, j].PlatformManager.PlatformRampManager.PlatformRampActive;
                platformStates[i, j].BridgeIsActive = grid.grid[i, j].PlatformManager.PlatformBridgeManager.PlatformBridgeActive;
                platformStates[i, j].PlatformSpawnActive = grid.grid[i, j].PlatformManager.PlatformSpawnManager.PlatformSpawnPointActive;
                platformStates[i, j].CurrentColour = grid.grid[i, j].PlatformManager.PlatformColourManager.CurrentColour;
                platformStates[i, j].CurrentBridgeHeight = grid.grid[i, j].PlatformManager.PlatformBridgeManager.CurrentBridgeHeight;
                platformStates[i, j].X = grid.grid[i, j].PlatformManager.PlatformStateManager.X;
                platformStates[i, j].Z = grid.grid[i, j].PlatformManager.PlatformStateManager.Z;
            }
        }
        return platformStates;
    }
    public List<PlatformInformation> ReturnStage(List<List<PlatformInformation>> level, int stageIndex)
    {
        var stageToReturn = level[stageIndex];
        return stageToReturn;
    }
    
    
    //Cant save the 2d array of nodes to a scriptable object 
    //I opted to convert all coordinate and state information in to integers that are stored in a list
    public List<PlatformInformation> ConvertToList(PlatformInformation[,] platformInformation)
    {
        var list = new List<PlatformInformation>();
        foreach (var go in platformInformation)
        {
            list.Add(go);
        }

        return list;
    }
    
    public void SaveStage(int stageIndex)
    {
        var _ = SaveStageInformation();
        var converted = ConvertToList(_);
        levelSo.SaveLevel(converted,stageIndex);
        
    }

    private void InsertStage(List<PlatformInformation> converted,int stageIndex)
    {
        CurrentLevelList[stageIndex] = converted;
    }

    public void LoadStage(int index)
    {
        GameManager.instance.EnvironmentManager.EnemySpawnPoints.internalSpawnPoints.Clear();
        SetCurrentStage(index);
        var levelToSet = levelSo.ReturnLevel(index);
                foreach (var go in levelToSet)
                {
                    var pos = grid.grid[go.X, go.Z].PlatformManager;
                    pos.PlatformStateManager.SetStateFromExternal(go);
                }
        

    }
    
    public void SetCurrentStage(int level)
    {
        CurrentStage = level;
    }


    
}
