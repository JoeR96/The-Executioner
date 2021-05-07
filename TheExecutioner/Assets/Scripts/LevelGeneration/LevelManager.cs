using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnvironmentManager environmentManager;
    [SerializeField] private List<LevelSo> Levels = new List<LevelSo>();
    public int CurrentLevel;
    public int CurrentStage;
    public bool menuMode;
    public List<List<PlatformInformation>> CurrentLevelList = new List<List<PlatformInformation>>();
    public bool BuildMode;
    private List<PlatformStateManager> spawnPoints = new List<PlatformStateManager>();
    public LevelSo levelSo;
    private Grid grid;
    private EnemySpawnPoints enemySpawnPoints;

    private void Start()
    {
        CurrentLevel = 0;
        CurrentStage = 0;
        enemySpawnPoints = GetComponent<EnemySpawnPoints>();
        grid = GetComponent<Grid>();
    }
    /// <summary>
    /// Load a random level from the level list
    /// set the current stage to 0
    /// </summary>
    public void LoadLevel()
    {
        CurrentStage = 0;
        
        var rand = Random.Range(0, Levels.Count);
        levelSo = Levels[rand];
    }
    /// <summary>
    /// Load a new stage of the level
    /// if the previous stage is the final stage of the level load a new one
    /// </summary>
    public void LoadStage()
    {
        if (CurrentStage == 2)
        {
            CurrentStage = 0;
            LoadLevel();
            LoadStage(CurrentStage);
        }

        else
        {
            CurrentStage++;
            LoadStage(CurrentStage);
        }
   
    }
    
    /// <summary>
    /// Load a certain stage of the level according to the input passed in
    /// </summary>
    /// <param name="index"></param>
    public void LoadStage(int index)
    {
        SetCurrentStage(index);
        var levelToSet = levelSo.ReturnLevel(index);

        foreach (var go in levelToSet)
        {
            if (grid.grid[go.X, go.Z] != null)
            {
                var pos = grid.grid[go.X, go.Z].PlatformManager;
                pos.PlatformStateManager.SetStateFromExternal(go);
                if (menuMode == false) 
                {
                    // pos.PlatformHeightManager.RaisePlatformTowerHeight();
                    if (pos.PlatformSpawnManager.PlatformEventSpawn)
                        enemySpawnPoints.AddEventSpawn(pos.PlatformSpawnManager.spawnPoint.transform);
                    if(pos.PlatformSpawnManager.PlatformSpawnPointActive)
                        GameManager.instance.EnvironmentManager.EnemySpawnPoints.CheckForSpawnPoint(
                            pos.PlatformStateManager.Node);
                }
            }
        }
    }
    /// <summary>
    /// Set the current stage index
    /// </summary>
    /// <param name="level"></param>
    public void SetCurrentStage(int level)
    {
        CurrentStage = level;
    }
    /// <summary>
    /// Save the current level to an input index
    /// </summary>
    /// <param name="stageIndex"></param>
    public void SaveStage(int stageIndex)
    {
        var _ = SaveStageInformation();
        var converted = ConvertToList(_);
        levelSo.SaveLevel(converted,stageIndex);
    }
    /// <summary>
    /// Save the state of each component on the platform to a platformInformation class
    /// </summary>
    /// <returns></returns>
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
                platformStates[i, j].PlatformEventSpawn = grid.grid[i, j].PlatformManager.PlatformSpawnManager.PlatformEventSpawn;
                platformStates[i, j].CurrentColour = grid.grid[i, j].PlatformManager.PlatformColourManager.CurrentColour;
                platformStates[i, j].CurrentBridgeHeight = grid.grid[i, j].PlatformManager.PlatformBridgeManager.CurrentBridgeHeight;
                platformStates[i, j].X = grid.grid[i, j].PlatformManager.PlatformStateManager.X;
                platformStates[i, j].Z = grid.grid[i, j].PlatformManager.PlatformStateManager.Z;
            }
        }
        return platformStates;
    }
    /// <summary>
    /// Return a  stage from the list of level stages using an index
    /// </summary>
    /// <param name="level"></param>
    /// <param name="stageIndex"></param>
    /// <returns></returns>
    public List<PlatformInformation> ReturnStage(List<List<PlatformInformation>> level, int stageIndex)
    {
        var stageToReturn = level[stageIndex];
        return stageToReturn;
    }
    /// <summary>
    /// Convert each platform information within the 2d array to a list
    /// Saving the X and Z coordinates means we do not need a 2d array and can simply serialize the data in a list format
    /// </summary>
    /// <param name="platformInformation"></param>
    /// <returns></returns>
    public List<PlatformInformation> ConvertToList(PlatformInformation[,] platformInformation)
    {
        var list = new List<PlatformInformation>();
        foreach (var go in platformInformation)
        {
            list.Add(go);
        }
        return list;
    }
    /// <summary>
    /// Build the navmesh at runtime
    /// </summary>
    public void BuildNavMesh()
    {
        environmentManager.BuildNavMesh();
    }



    
}
