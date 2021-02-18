using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EdgeSpawnPlatform : MonoBehaviour
{
    private RoomManager roomManager;
    private NavMeshSurface navMeshSurface;
    
    private void Awake()
    {
        roomManager = GetComponent<RoomManager>();
    }
    
    public List<GameObject[,]> EntranceSpawnSpawners(GameObject[,] tileArray)
    {
        List<GameObject[,]> EntranceSpawnPlatform = new List<GameObject[,]>();
        var a = roomManager.FillRoomArray(tileArray,0, 0,2,2);
        var b = roomManager.FillRoomArray(tileArray,15, 0,2,2);
        var c = roomManager.FillRoomArray(tileArray,0, 15,2,2);
        var d = roomManager.FillRoomArray(tileArray,15, 15,2,2);
        
        EntranceSpawnPlatform.Add(a);
        EntranceSpawnPlatform.Add(b);
        EntranceSpawnPlatform.Add(c);
        EntranceSpawnPlatform.Add(d);

        return EntranceSpawnPlatform;
    }
}
