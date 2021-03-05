using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditor.Searcher;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    

    [SerializeField] LevelSo levelSo;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        Debug.Log(grid);
    }
    
    
    public void SaveStageOne()
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
                platformStates[i, j].X = grid.grid[i, j].PlatformState.X;
                platformStates[i, j].Z = grid.grid[i, j].PlatformState.Z;
            }
        }
        levelSo.AddLevel(platformStates);
        AssetDatabase.SaveAssets();
    }

    public void ClearSo()
    {
        levelSo.Levels.Clear();
        levelSo.LevelCount = 0;
    }
    public void LoadStage(int index)
    {
        PlatformInformation[,] levelToSet = levelSo.Levels[index];
        
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            for (int j = 0; j < grid.gridSizeY; j++)
            {
                var newPlatformState = levelToSet[i, j];
                
                grid.grid[i,j].PlatformState.SetStateFromExternal(  
                    newPlatformState.CurrentHeight,
                    newPlatformState.CurrentRotation,
                    newPlatformState.PlatformStairActive);
                grid.grid[i,j].PlatformState.SetState();
            }
        }
        

    }
    
}
