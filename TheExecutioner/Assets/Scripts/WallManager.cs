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
        startX = Random.Range(2, 13);
        int startZ; 
        startZ = Random.Range(2, 13);
        //get length of wall
        int wallOneLength = Random.Range(2, 4);
        int wallTwoLength = Random.Range(2, 4);
        wall = FillWallArray(tileArray,wallOneLength, wallTwoLength, startX, startZ);
        return wall;
    }
    
    private GameObject[,] FillWallArray(GameObject[,] tileArray,int xWallLength, int zWallLength,int xStart, int zStart)
    {
        GameObject[,] wall = new GameObject[xWallLength,zWallLength];
        int counter = 0;
        for (int x = 0; x < xWallLength; x++)
        {
            for (int z = 0; z < zWallLength; z++)
            {
                counter++;
                if (tileArray[xStart + x, zStart + z])
                    wall[x, z] = tileArray[xStart + x, zStart + z];
                
            }
        }

        return wall;
    }
}
