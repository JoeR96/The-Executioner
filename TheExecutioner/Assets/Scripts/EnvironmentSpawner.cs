using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{

    public GameObject[,] SpawnGrid(GameObject floor, int gridX, int gridZ, int y, int gridSpaceOffset,
        Transform cubeParent)
    {
        Vector3 gridOrigin = new Vector3(-2,0,-2); 
        GameObject[,] tileArray = new GameObject[gridX,gridZ];;

        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpaceOffset,y, z * gridSpaceOffset ) + gridOrigin;
                var _  = SpawnChunk(floor,spawnPosition,Quaternion.identity);
                _.transform.SetParent(cubeParent);
                tileArray[x, z] = _;
                tileArray[x,z].GetComponent<PlatformState>().Setint(x,z);
            }
            
        }
        return tileArray;
    }
    
    protected GameObject SpawnChunk(GameObject floor,Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        var chunk = Instantiate(floor, positionToSpawn, rotationToSpawn);
        return chunk;
    }

}
