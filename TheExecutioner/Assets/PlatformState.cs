using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlatformState : MonoBehaviour
{
    [SerializeField] private GameObject stairs;
    [SerializeField] private GameObject raycastHolder;
    [SerializeField] private int boundarySize;
    public int X;
    public int Z;

    public GameObject[,] connectingPlatforms = new GameObject[2,2];

    public bool PlatformInUse = false;

    private void Start()
    {

        connectingPlatforms[1, 1] = gameObject;
    }

    private void SpawnStairs()
    {
        var adjacent = CheckAdjacentPositions();
        foreach (var VARIABLE in adjacent)
        {
            VARIABLE.transform.position = Vector3.zero;
        }
    }
    public bool clicked = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            FillGaps();

        }
    }

    private void FillGaps()
    {
        if (PlatformInUse)
        {
            var t = CheckAdjacentPositions();
            if (Random.value < 0.25)
            {
                Iterate(t);
            }
            
        }

    }
    public void SetState(bool b)
    {
        PlatformInUse = b;
    }

    public bool ReturnState()
    {
        return PlatformInUse;
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
    private void Iterate(GameObject[,] tileArray)
    {
        int counter = 0;
        List<GameObject> tempList = new List<GameObject>();

        for (int row = 0; row < tileArray.GetLength(0); row++)
        {
            for (int column = 0; column < tileArray.GetLength(1); column++)
            {
                GameObject adjacentPlatform = tileArray[row, column];
                tempList.Add(adjacentPlatform);
            }
        }
        
        StartCoroutine(SpawnInGap(tempList));
        
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList.RemoveAt(i);
        }
        
        
    }

    private IEnumerator SpawnInGap(List<GameObject> tempList)
    {
        for (int i = 0; i < tempList.Count -1; i++)
        {
            if (Math.Abs(tempList[i].transform.position.y - transform.position.y) > 0.25f)
            {
                raycastHolder.transform.position = tempList[i].transform.position;
                if (Random.value > 0.0625f)
                {
                    GameManager.instance.EnvironmentManager.RaisePlatform(tempList[i]);
                }
                else
                {
                    var targetPosition = tempList[i].transform.position;
                    targetPosition.y += 18f;
                    var stair = Instantiate(stairs, targetPosition, quaternion.identity);
                    stair.GetComponent<StairCheck>().InvokeStairs();

                }
                raycastHolder.transform.position = tempList[i].transform.position;
                
            }
        }
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.EnvironmentManager.BuildNavMesh();
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
            Debug.DrawRay(origin,hit.point,Color.red,Mathf.Infinity);
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


