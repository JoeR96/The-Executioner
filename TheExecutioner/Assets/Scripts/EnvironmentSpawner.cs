using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentSpawner : MonoBehaviour
{
    public Material[] Materials;
    public GameObject stairs;
    private Pathfinding pathfinding;
    private PlatformManager platformManager;
    private Grid grid;
    
    private List<GameObject> Stairs = new List<GameObject>();
    private List<List<GameObject[,]>> LevelPlatforms = new List<List<GameObject[,]>>();
    public List<List<List<Node>>> LevelPaths = new List<List<List<Node>>>();
    public List<List<Node>> LevelBunkers = new List<List<Node>>();
    public List<List<Node>> LevelHighBunkers = new List<List<Node>>();

    private void Start()
    {
        platformManager = GetComponent<PlatformManager>();
        grid = GetComponent<Grid>();
    }
    public List<Node> ReturnRandomPath()
    {
        var random = Random.Range(0, LevelBunkers.Count);
        var path = LevelBunkers[random];
        return path;
    }
    private void Awake()
    {
        LevelPaths.Add(LevelBunkers);
        LevelPaths.Add(LevelHighBunkers);
    }
    private List<Node> GetPath()
    {
        
        pathfinding = GameManager.instance.pathfinding;
        var path = pathfinding.ReturnPath();
        return path;
    }
    public List<Node> SmoothPath(List<Node> path)
    {
        var smoothTiles = new List<Node>();
        foreach (var tile in path)
        {
            var t = CheckAdjacentPositions(tile);
            bool checkOne, checkTwo;
            if (t[0,0].platform.GetComponent<PlatformState>().PlatformIsActive)
            {
                var x = SmoothCheck(1, 0, 0, 1, t);
                
                if (x.Count == 2)
                {
                    if (Random.value > 0.5f)
                    {
                        x.RemoveAt(0);
                    }
                    else
                    {
                        x.RemoveAt(1);
                    }
                }
                for (int i = 0; i < x.Count; i++)
                {
                    smoothTiles.Add(x[i]);
                    
                }
            }

            if (t[2, 2].platform.GetComponent<PlatformState>().PlatformIsActive)
            {
                var x = SmoothCheck(2, 1, 1, 2, t);
             

                if (x.Count == 2)
                {
                    if (Random.value > 0.5f)
                    {
                        x.RemoveAt(0);
                    }
                    else
                    {
                        x.RemoveAt(1);
                    }
                }
                for (int i = 0; i < x.Count; i++)
                {
                    smoothTiles.Add(x[i]);
                }
            }
            if (t[0, 2].platform.GetComponent<PlatformState>().PlatformIsActive)
            {
                var x = SmoothCheck(0, 1, 1, 2, t);
                for (int i = 0; i < x.Count; i++)
                {
                    smoothTiles.Add(x[i]);
                }
                if (x.Count == 2)
                {
                    if (Random.value > 0.5f)
                    {
                        x.RemoveAt(0);
                    }
                    else
                    {
                        x.RemoveAt(1);
                    }
                }
            }
            if (t[2, 0].platform.GetComponent<PlatformState>().PlatformIsActive)
            {
                var x = SmoothCheck(1, 1, 2, 1, t);
                for (int i = 0; i < x.Count; i++)
                {
                    smoothTiles.Add(x[i]);
                }
                if (x.Count == 2)
                {
                    if (Random.value > 0.5f)
                    {
                        x.RemoveAt(0);
                    }
                    else
                    {
                        x.RemoveAt(1);
                    }
                }
            }
        }
        
        return smoothTiles;

    }
    private List<Node> SmoothCheck(int x, int z,int j,int k, Node[,] path)
    {
        {
            var temp = new List<Node>();
                
            var pos = path[x, z].platform.GetComponent<PlatformState>();
                    
            var posTwo = path[j,k].platform.GetComponent<PlatformState>();
                    
            var checkOne = pos.PlatformIsActive;
            var checkTwo = pos.PlatformIsActive;

            var platformCheck = pos.PlatformIsPlatform;
            var platformCheckTwo = posTwo.PlatformIsPlatform;
                
                
            if(!checkOne)
                temp.Add(pos.Node);
            if(!checkTwo)
                temp.Add(posTwo.Node);
                
            Debug.Log(temp.Count);
            return temp;
        }
    }
    public void SpawnHighBunkers(List<List<Node>> Levels)
    {
        var path = GetPath();
        var toAdd = SmoothPath(path);
        
        for (int i = 0; i < toAdd.Count; i++)
        {
            path[i].platform.GetComponent<PlatformState>().PlatformIsActive = false;
            path.Add(toAdd[i]);

        }
        
        Levels.Add(path);
        pathfinding.InitializePath();
        var material = Random.Range(0, Materials.Length);
        //ChangePathColor(Materials[material],LevelBunkers);
        foreach (var go in Levels)
        {    
       
            foreach (var VARIABLE in go)
            {
                if (!VARIABLE.InUse)
                {
                    
                    VARIABLE.platform.GetComponent<PlatformState>().PlatformIsActive = true;
                    VARIABLE.platform.GetComponent<MeshRenderer>().material = Materials[material]; 
                    platformManager.RaiseHighPlatform(VARIABLE.platform);
                    VARIABLE.platform.layer = 11;
                    LevelBunkers.Add(go);
                }
                
            }
        }
        SpawnHighStairs(path);
        if (Random.value < 0.5f)
        {
            SpawnHighStairs(path);
        }
        
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
    private void ChangePathColor(Material material,List<List<Node>> list)
    {
        foreach (var go in list)
        {
            foreach (var g in go)
            {
                g.platform.GetComponent<MeshRenderer>().material = material;
            }
        }
    }
    public void SpawnBunkers(List<List<Node>> levels)
    {
        var path = GetPath();
        var toAdd = SmoothPath(path);
        
        for (int i = 0; i < toAdd.Count; i++)
        {
            path[i].platform.GetComponent<PlatformState>().PlatformIsActive = false;
            path.Add(toAdd[i]);

        }
        levels.Add(path);
        
        pathfinding.InitializePath();
        var material = Random.Range(0, Materials.Length);
        ChangePathColor(Materials[material],LevelBunkers);

        foreach (var VARIABLE in path)
        {
            if (!VARIABLE.InUse)
            {
                VARIABLE.platform.GetComponent<PlatformState>().PlatformIsActive = true;
                VARIABLE.platform.GetComponent<MeshRenderer>().material = Materials[material];
                VARIABLE.platform.layer = 11;
                platformManager.RaisePlatform(VARIABLE.platform);
                LevelBunkers.Add(path);
            }
                
        }
        SpawnStairs(path);
        if (Random.value < 0.5f)
        {
            SpawnStairs(path);
        }
        Debug.Log(path.Count);
    }
    
    public void SpawnConnectingBunkers(List<List<Node>> Levels,List<Node> pathToRaise)
    {
        var path = pathToRaise;

        Levels.Add(path);
        pathfinding.InitializePath();
        var material = Random.Range(0, Materials.Length);
        //ChangePathColor(Materials[material],LevelBunkers);
        foreach (var go in Levels)
        {    
            foreach (var VARIABLE in go)
            {
                if (!VARIABLE.InUse)
                {
                    VARIABLE.platform.GetComponent<PlatformState>().PlatformIsActive = true;
                    VARIABLE.platform.GetComponent<MeshRenderer>().material = Materials[material]; 
                    platformManager.RaisePlatform(VARIABLE.platform);
                }
                    
            }
        }
        SpawnStairs(path);
    }
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
        return platformGroup[ x,  y].platform.GetComponent<PlatformState>().PlatformIsActive;
    }
    public void SpawnStairs(List<Node> path)
         {
             List<Tuple<Vector2, float>> spawnPositions = new List<Tuple<Vector2, float>>();
     
             Tuple<Vector2, float> GetStairSpawn(Vector2 position, float rotation)
             {
                 float quaternion = rotation;
                 var vector = position;
                 return new Tuple<Vector2, float>(vector, quaternion);
             }
     
             var node = GetNode(path);
             var adjacent = CheckAdjacentPositions(node);
     
             //The purpose of checking twice here is to ensure there is nothing blocking the platform leading to the stairs
             if (!ReturnStairSpawnStatus(2, 1, adjacent))
             {
                 if (!ReturnStairSpawnStatus(2, 0, adjacent))
                 {
                     spawnPositions.Add(GetStairSpawn(new Vector2(2, 1), 0));
                 }
             }
             if (!ReturnStairSpawnStatus(1, 2, adjacent))
             {
                 if (!ReturnStairSpawnStatus(0, 2, adjacent))
                 {
                     spawnPositions.Add(GetStairSpawn(new Vector2(1, 2), 90));
                 }
             }
             if (!ReturnStairSpawnStatus(3, 2, adjacent))
             {
                 if (!ReturnStairSpawnStatus(4, 2, adjacent))
                 {
                     spawnPositions.Add(GetStairSpawn(new Vector2(3, 2), 270));
                 }
             }
             if (!ReturnStairSpawnStatus(2, 3, adjacent))
             {
                 if (!ReturnStairSpawnStatus(2, 4, adjacent))
                 {
                     spawnPositions.Add(GetStairSpawn(new Vector2(2, 3), 180));
                 }
             }
     
     
             if (spawnPositions.Count != 0)
             {
                 var random = Random.Range(0, spawnPositions.Count);
                 var spawnPosition = spawnPositions[random];
                 var pos = spawnPosition.Item1;
     
                 var t = Instantiate(stairs,
                     adjacent[(int) pos.x, (int) pos.y].worldPosition,
                     quaternion.identity);
     
                 var x = t.GetComponent<PlatformState>();
                 x.PlatformIsActive = true;
                 foreach (var adjacentt in adjacent)
                 {
                     adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[2];
                     if(!adjacentt.platform.GetComponent<PlatformState>().PlatformIsActive)
                     {
                         adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[3];
                     }
                 }
                 adjacent[2, 2].platform.GetComponent<MeshRenderer>().material = Materials[2];
                 x.Setint(adjacent[0,0].gridX -1 +(int)pos.x,adjacent[0,0].gridY+(int)pos.y -1);
                  t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                     transform.localRotation.eulerAngles.y + spawnPosition.Item2,
                     transform.localRotation.eulerAngles.z);
                  var newPos = new Vector3(t.transform.position.x + 2, t.transform.position.y +2.5f, t.transform.position.z - 2);
                  t.transform.position = newPos;
                  t.GetComponent<PlatformState>().PlatformIsPlatform = false;
             }
         }
    public void SpawnHighStairs(List<Node> path)
    {
        List<Tuple<Vector2, float>> spawnPositions = new List<Tuple<Vector2, float>>();

        Tuple<Vector2, float> GetStairSpawn(Vector2 position, float rotation)
        {
            float quaternion = rotation;
            var vector = position;
            return new Tuple<Vector2, float>(vector, quaternion);
        }

        var node = GetNode(path);
        var adjacent = CheckAdjacentPositions(node);

        //The purpose of checking twice here is to ensure there is nothing blocking the platform leading to the stairs
        if (!ReturnStairSpawnStatus(2, 1, adjacent))
        {
            if (!ReturnStairSpawnStatus(2, 0, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(2, 1), 0));
            }
        }
        if (!ReturnStairSpawnStatus(1, 2, adjacent))
        {
            if (!ReturnStairSpawnStatus(0, 2, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(1, 2), 90));
            }
        }
        if (!ReturnStairSpawnStatus(3, 2, adjacent))
        {
            if (!ReturnStairSpawnStatus(4, 2, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(3, 2), 270));
            }
        }
        if (!ReturnStairSpawnStatus(2, 3, adjacent))
        {
            if (!ReturnStairSpawnStatus(2, 4, adjacent))
            {
                spawnPositions.Add(GetStairSpawn(new Vector2(2, 3), 180));
            }
        }


        if (spawnPositions.Count != 0)
        {
            var random = Random.Range(0, spawnPositions.Count);
            var spawnPosition = spawnPositions[random];
            var pos = spawnPosition.Item1;
            GameManager.instance.EnvironmentManager.PlatformManager.RaisePlatform(adjacent[(int) spawnPosition.Item1.x,(int) spawnPosition.Item1.y].platform);
            var t = Instantiate(stairs,
                adjacent[(int) pos.x, (int) pos.y].worldPosition,
                quaternion.identity);

            var x = t.GetComponent<PlatformState>();
            x.PlatformIsActive = true;
            
            foreach (var adjacentt in adjacent)
            {
                adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[2];
                if(!adjacentt.platform.GetComponent<PlatformState>().PlatformIsActive)
                {
                    adjacentt.platform.GetComponent<MeshRenderer>().material = Materials[3];
                }
            }
            adjacent[2, 2].platform.GetComponent<MeshRenderer>().material = Materials[2];
            x.Setint(adjacent[0,0].gridX -1 +(int)pos.x,adjacent[0,0].gridY+(int)pos.y -1);
             t.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y + spawnPosition.Item2,
                transform.localRotation.eulerAngles.z);
             var newPos = new Vector3(t.transform.position.x + 2, t.transform.position.y +7.5f, t.transform.position.z - 2);
             t.transform.position = newPos;
             t.GetComponent<PlatformState>().SpawnPath();
             t.GetComponent<PlatformState>().Setint(adjacent[0,0].gridX -1 +(int)pos.x,adjacent[0,0].gridY+(int)pos.y);
        }
    }
    public Node[,] CheckAdjacentPositions(Node node)
    {
        
        Node[,] adjacent = new Node[5,5];

        var startX = node.gridX -2;
        var startZ = node.gridY -2;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1) ; z++)
            {
      
                adjacent[x , z ] = grid.grid[startX +x, startZ + z];
                if(grid.grid[startX + x, startZ + z] == null)
                {
                    Debug.Log(startX);
                    Debug.Log(x);
                    Debug.Log(startZ);
                    Debug.Log(z);
                }
            }
        }
        return adjacent;
    }

}
