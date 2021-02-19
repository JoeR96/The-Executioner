using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[HideInInspector]
public class RoomManager : MonoBehaviour
{
    public GameObject[,] FillRoomArray(GameObject[,] tileArray, int targetX, int targetZ,int roomXsize, int roomZsize)
    {
        GameObject[,] room = new GameObject[roomXsize,roomZsize];
        
        for (int x = 0; x < roomXsize; x++)
        {
            for (int z = 0; z < roomZsize; z++)
            {
                room[x, z] = tileArray[targetX + x , targetZ + z];
            }
        }
        return room;
    }
    
    private GameObject[,] SpawnArena(GameObject[,] tileArray)
    {

        int xSpawn = Random.Range(3, 7);
        int zSpawn = Random.Range(3, 7);

        int xSize = Random.Range(7, 10);
        int zSize = Random.Range(8, 14);

        var room = FillRoomArray(tileArray,xSpawn, zSpawn, xSize, zSize);
        return room;
    }

    public GameObject[,] SpawnRandomRoom(int xGridSize, int zGridSize)
    {
        var RoomSize = GetRandomRoomSize();
        var RoomPositions = GetRoomStartPosition(xGridSize,zGridSize);
        
        int xSpawn = RoomPositions.Item1;
        int zSpawn = RoomPositions.Item2;

        int xSize = RoomSize.Item1;
        int zSize = RoomSize.Item2;

        var room = new GameObject[xSize,zSize];
        room = FillRoomArray(room,xSpawn, zSpawn, xSize, zSize);
        return room;
    }
    private Tuple<int,int> GetRoomStartPosition(int gridX, int gridZ)
    {
        int roomX = Random.Range(1, gridX - 1);
        int roomZ = Random.Range(1, gridZ - 1);
        return new Tuple<int, int>(roomX, roomZ);
    }
    
    private Tuple<int,int> GetRandomRoomSize()
    {
        int roomSizeX = Random.Range(2, 5);
        int roomSizeZ = Random.Range(2, 7);
        return new Tuple<int, int>(roomSizeX, roomSizeZ);
    }
}
