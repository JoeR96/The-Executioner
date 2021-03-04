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
        PlatformState[,] platformStates = new PlatformState[grid.gridSizeX, grid.gridSizeY];
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            for (int j = 0; j < grid.gridSizeY; j++)
            {
                platformStates[i, j] = grid.grid[i, j].PlatformState;
            }
        }
        
        levelSo.Levels.Add(platformStates);
        levelSo.LevelCount = levelSo.Levels.Count;
    }

    public void ClearSo()
    {
        levelSo.Levels.Clear();
        levelSo.LevelCount = 0;
    }
    public void LoadStage(int index)
    {
        PlatformState[,] levelToSet = levelSo.Levels[index];
        for (int i = 0; i < grid.gridSizeX; i++)
        {
            for (int j = 0; j < grid.gridSizeY; j++)
            {
                var t = levelToSet[i, j];
                grid.grid[i,j].PlatformState.SetStateFromExternal(
                    t.CurrentHeight,t.CurrentRotation,t.PlatformStairActive);
                if (t.CurrentHeight != PlatformHeight.Flat)
                {
                    Debug.Log(t.CurrentHeight + " TITS");
                }
                t.SetState();
            }
        }

    }
}
