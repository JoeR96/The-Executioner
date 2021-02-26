using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;



[HideInInspector]
public class EnvironmentManager : MonoBehaviour
{

    
    public Transform NavMeshObject;
    private GameObject[,] _tileArray;
    private WallManager wallManager;
    private RoomManager roomManager;
    public Pathfinding pathFinding;
    private NavMeshSurface navmeshSurface;
    public PlatformManager PlatformManager;
    public EnvironmentSpawner environmentSpawner;
    public Grid grid;



 




    public void LowerAll()
    {

        foreach (var go in environmentSpawner.LevelPaths)
        {
            foreach (var t in go)
            {
                foreach (var x in t)
                {
                 GameManager.instance.EnvironmentManager.PlatformManager.LowerPlatform(x.platform);   
                }
            }
            
        }

    }

    private void Awake()
    {
        
        
        environmentSpawner = GetComponent<EnvironmentSpawner>();
        pathFinding = GetComponent<Pathfinding>();
        navmeshSurface = NavMeshObject.GetComponent<NavMeshSurface>();
        wallManager = GetComponent<WallManager>();
        roomManager = GetComponent<RoomManager>();
        PlatformManager = GetComponent<PlatformManager>();

        grid = GetComponent<Grid>();
    }
    
    

    

    public void StartBunkers()
    {
        environmentSpawner.SpawnBunkers(environmentSpawner.LevelBunkers);
    }
    public void StartHighBunkers()
    {
        environmentSpawner.SpawnHighBunkers(environmentSpawner.LevelHighBunkers);
    }


    
    
    private void DiagonalCheck()
    {

    }
    
    
    
    public void BuildNavMesh()
    {
        navmeshSurface.BuildNavMesh();
    }

    

    
    
    
    
    

    

    public GameObject[,] ReturnMap()
    {
        return _tileArray;
    }

    
    
    // public void LowerLastPlatform()
    // {
    //     StartCoroutine(LowerNegativePlatform());
    // }
    // public IEnumerator LowerNegativePlatform()
    // {
    //     var path = ReturnRandomPath();
    //     for (int i = path.Count - 1; i >= 0; i--)
    //     {
    //         PlatformManager.LowerPlatform(path[i].platform);
    //         yield return new WaitForSeconds(0.25f);
    //     }
    // }
}
    


