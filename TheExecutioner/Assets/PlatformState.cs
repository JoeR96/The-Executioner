﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlatformState : MonoBehaviour
{
    [SerializeField] private List<GameObject> randomSpawnPickups = new List<GameObject>();
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject raycastHolder;
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] private int boundarySize;

    public bool SpawnInUse;
    public int X;
    public int Z;

    public GameObject[,] connectingPlatforms = new GameObject[2,2];

    public bool PlatformInUse = false;

    private void Start()
    {
        var x = Random.Range(0, 16);
        if (x == 15)
        {
            var t = Instantiate(randomSpawnPickups[0],spawnPoint.transform.position,Quaternion.identity);
            t.transform.SetParent(spawnPoint.transform);
        }

        if (x == 10)
        {
            var t = Instantiate(randomSpawnPickups[1],spawnPoint.transform.position,Quaternion.identity);
            t.transform.SetParent(spawnPoint.transform);
        }
            
        connectingPlatforms[1, 1] = gameObject;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnStairs();

        }
    }

    

    private GameObject[,] CheckAdjacentPositions()
    {
        var tileMap = GameManager.instance.EnvironmentManager.ReturnMap();
        GameObject[,] adjacent = new GameObject[3,3];
        int tracker = 0;
        
        
        var startX = X - 1;
        var startZ = Z - 1;
        
        for (int x = 0; x < adjacent.GetLength(0) ; x++)
        {
            for (int z = 0; z < adjacent.GetLength(1); z++)
            {
                adjacent[x, z] = tileMap[startX +x, startZ + z];
            }
        }
        
        return adjacent;
    }

    private void SetPosition(int x, int z)
    {
        raycastHolder.transform.position = new Vector3(x,transform.position.y,z);
    }
    public void SpawnStairs()
    {
        StartCoroutine(SpawnStair());

    }

    private IEnumerator SpawnStair()
    {
        var possibleSpawns = new List<GameObject>();
        float y = transform.position.y;
        var adjacent = CheckAdjacentPositions();
        foreach (var go in adjacent)
        {
            var distance = y - go.transform.position.y;

           
            if (distance > 4 & distance <= 6)
            {
                possibleSpawns.Add(go.GetComponent<PlatformState>().spawnPoint);
            }
            
            //GameManager.instance.EnvironmentManager.BuildNavMesh();
            yield return null;
        }
        var spawn = Random.Range(0, possibleSpawns.Count -1);
        var spawnPosition = possibleSpawns[spawn];
        var transformPosition = spawnPosition.transform.position;
        transformPosition.y = transformPosition.y - 5f;
        
        var stair = Instantiate(stairs, transformPosition, quaternion.identity);
        SpawnInUse = true;
        stair.transform.SetParent(transform);
        GameManager.instance.EnvironmentManager.RaisePlatform(stair);
    }

    private GameObject FireRay(int xPos, int zPos)
    {
        raycastHolder.transform.position = new Vector3(xPos,transform.position.y,zPos);
        
        RaycastHit hit;
        float thickness = 1f; //<-- Desired thickness here.
        Vector3 origin = raycastHolder.transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        
        if (Physics.SphereCast(origin, thickness, direction, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
}


