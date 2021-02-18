using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public class WallManager : MonoBehaviour
{

    public GameObject[,] SpawnRandomWall(GameObject[,] tileArray)
    {
        GameObject[,] wall;
        int startX; 
        startX = Random.Range(2, 14);
        int startZ; 
        startZ = Random.Range(2, 14);
        //get length of wall
        int wallOneLength = Random.Range(1, 5);
        int wallTwoLength = Random.Range(1, 5);
        wall = FillWallArray(tileArray,wallOneLength, wallTwoLength, startX, startZ);
        return wall;
    }
    
    private GameObject[,] FillWallArray(GameObject[,] tileArray,int xWallLength, int zWallLength,int xStart, int zStart)
    {
        GameObject[,] wall = new GameObject[xWallLength,zWallLength];
        for (int x = 0; x < xWallLength ; x++)
        {
            for (int z = 0; z < zWallLength ; z++)
            {
                if (Random.value > 0.5f)
                {
                    wall[x, z] = tileArray[xStart , zStart + z];
                }
                else
                {
                    wall[x, z] = tileArray[xStart + x, zStart];    
                }
            }
        }

        return wall;
    }
}
