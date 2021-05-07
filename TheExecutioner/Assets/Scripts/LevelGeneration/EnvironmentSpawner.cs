using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentSpawner : MonoBehaviour
{
 
    private Pathfinding pathfinding;
    private Grid grid;
    
    public List<List<List<Node>>> LevelPaths = new List<List<List<Node>>>();
    public List<List<Node>> LevelBunkers = new List<List<Node>>();
    public List<List<Node>> LevelHighBunkers = new List<List<Node>>();
   
    

    private void Start()
    {
        grid = GetComponent<Grid>();
    }


    private List<Node> GetPath()
    {
        
        pathfinding = GameManager.instance.Pathfinding;
        var path = pathfinding.ReturnPath();
        return path;
    }
    
    public Node GetNode(List<Node> path)
    {
        return GetRandomNode(path);
    }
    public Node GetRandomNode(List<Node> nodes)
    {
        var random = Random.Range(0, nodes.Count );
  
        var node = nodes[random ];
        if (node == null)
        {
        }
        return node;
    }

    public void SpawnPath( int height)
    {
        var path = GetPath();
        
        pathfinding.InitializePath();
        var target = height;
        foreach (var node in path)
        {
            var t = node.platform.GetComponent<PlatformManager>();
                t.PlatformStateManager.PlatformIsActive = true;
                t.PlatformHeightManager.SetPlatformHeight(target);
                LevelBunkers.Add(path);
                
        }
    }
    #region StairSpawningLogic
    //I stopped building upon this logic once I built the custom editor
    //I would improve the stair spawning logic if time allowed 
    //Manually setting the stairs allowed me to progress quickly with the project and manage scope more effectively
    private bool ReturnStairSpawnStatus(int x,int y,Node[,] platformGroup)
    {
        if (platformGroup[x, y] == null)
        {
            foreach(var go in platformGroup)
                Debug.Log(go.GridPosition);
            
            Debug.Log(platformGroup[2,2]);
            Debug.Log(x);
            Debug.Log(y);
        }
        return platformGroup[ x,  y].platform.GetComponent<PlatformStateManager>().PlatformIsActive;
    }
   
    
    // public void SpawnStairs(List<Node> path)
    //      {
    //          List<Tuple<Vector2, float>> spawnPositions = new List<Tuple<Vector2, float>>();
    //  
    //          Tuple<Vector2, float> GetStairSpawn(Vector2 position, float rotation)
    //          {
    //              float quaternion = rotation;
    //              var vector = position;
    //              return new Tuple<Vector2, float>(vector, quaternion);
    //          }
    //  
    //          var node = GetNode(path);
    //          var adjacent = CheckAdjacentNodes.CheckAdjacentPositions(node,grid.grid);
    //  
    //          //The purpose of checking twice here is to ensure there is nothing blocking the platform leading to the stairs
    //          if (!ReturnStairSpawnStatus(2, 1, adjacent))
    //          {
    //              if (!ReturnStairSpawnStatus(2, 0, adjacent))
    //              {
    //                  spawnPositions.Add(GetStairSpawn(new Vector2(2, 1), 0));
    //              }
    //          }
    //          if (!ReturnStairSpawnStatus(1, 2, adjacent))
    //          {
    //              if (!ReturnStairSpawnStatus(0, 2, adjacent))
    //              {
    //                  spawnPositions.Add(GetStairSpawn(new Vector2(1, 2), 90));
    //              }
    //          }
    //          if (!ReturnStairSpawnStatus(3, 2, adjacent))
    //          {
    //              if (!ReturnStairSpawnStatus(4, 2, adjacent))
    //              {
    //                  spawnPositions.Add(GetStairSpawn(new Vector2(3, 2), 270));
    //              }
    //          }
    //          if (!ReturnStairSpawnStatus(2, 3, adjacent))
    //          {
    //              if (!ReturnStairSpawnStatus(2, 4, adjacent))
    //              {
    //                  spawnPositions.Add(GetStairSpawn(new Vector2(2, 3), 180));
    //              }
    //          }
    //  
    //  
    //          if (spawnPositions.Count != 0)
    //          {
    //              var random = Random.Range(0, spawnPositions.Count);
    //              var spawnPosition = spawnPositions[random];
    //              var pos = spawnPosition.Item1;
    //             
    //              adjacent[(int) pos.x,(int) pos.y].PlatformManager.PlatformRampManager.ActivateRamp(true);
    //              
    //          }
    //      }
    #endregion
}
