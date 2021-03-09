﻿using System.Collections;
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
    public LevelSo levelSo;
    private Grid grid;
    // Start is called before the first frame update
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
                platformStates[i, j].CurrentHeight = grid.grid[i, j].PlatformState.CurrentHeight;
                platformStates[i, j].CurrentRotation = grid.grid[i, j].PlatformState.CurrentRotation;
                platformStates[i, j].PlatformStairActive = grid.grid[i, j].PlatformState.PlatformStairActive;
                platformStates[i, j].BridgeIsActive = grid.grid[i, j].PlatformState.PlatformBridgeActive;
                platformStates[i, j].CurrentColour = grid.grid[i, j].PlatformState.CurrentColour;
                platformStates[i, j].CurrentBridgeHeight = grid.grid[i, j].PlatformState.CurrentBridgeHeight;
                platformStates[i, j].X = grid.grid[i, j].PlatformState.X;
                platformStates[i, j].Z = grid.grid[i, j].PlatformState.Z;
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
        SetCurrentStage(index);W
        var levelToSet = levelSo.ReturnLevel(index);
                foreach (var go in levelToSet)
            {
                grid.grid[go.X,go.Z].PlatformState.SetStateFromExternal(go.CurrentHeight,go.CurrentRotation,
                    go.PlatformStairActive,go.CurrentBridgeHeight,go.BridgeIsActive,go.CurrentColour);
                grid.grid[go.X,go.Z].PlatformState.SetState();
            }
        

    }
    
    public void SetCurrentStage(int level)
    {
        CurrentStage = level;
    }
    
    
}
