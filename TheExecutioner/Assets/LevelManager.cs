using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelSo levelSo;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
    }

    public void SaveStageOne()
    {
        Node[,] newLevel = new Node[grid.gridSizeX, grid.gridSizeY];
        for (int i = 0; i < grid.grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.grid.GetLength(1); j++)
            {
                newLevel[i, j] = grid.grid[i, j];
            }
        }
        Debug.Log(levelSo.Levels.Count);
        levelSo.Levels.Add(newLevel);
        levelSo.LevelCount = levelSo.Levels.Count;
    }

    public void LoadStage(int index)
    {
        Node[,] levelToSet = levelSo.Levels[index];
        grid.grid = levelToSet;
        for (int i = 0; i < grid.grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.grid.GetLength(1); j++)
            {
                grid.grid[i, j].PlatformState.currentHeight 
                    = levelToSet[i, j].PlatformState.currentHeight;
       
                grid.grid[i,j].PlatformState.SetState();
            }
        }

    }
}
