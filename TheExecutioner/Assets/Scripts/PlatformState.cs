using System;
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

    public bool PlatformIsPlatform = true;
    public bool PlatformIsActive = false;
    public int X;
    public int Z;

    public GameObject[,] connectingPlatforms = new GameObject[2,2];
    

    private void Start()
    {
        var x = Random.Range(0, 16);
        if (x == 14)
        {
            var t = Instantiate(randomSpawnPickups[0],spawnPoint.transform.position,Quaternion.identity);
            t.transform.SetParent(spawnPoint.transform);
        }
        
        if (x == 10)
        {
            var t = Instantiate(randomSpawnPickups[1],spawnPoint.transform.position,Quaternion.identity);
            t.transform.SetParent(spawnPoint.transform);
        }
        
    }

    [SerializeField] private GameObject cubeTing;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && !PlatformIsPlatform)
        {
            FireSphere();
            
        }
    }
    private GameObject FireSphere()
    {
        RaycastHit hit;
        float thickness = 24f; //<-- Desired thickness here.
        Vector3 origin = raycastHolder.transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        
        if (Physics.SphereCast(origin, thickness, direction, out hit))
        {
            Instantiate(cubeTing, hit.point, quaternion.identity);
            return hit.collider.gameObject;
        }

        return null;
    }
    public void SetPlatformState(bool state)
    {
        PlatformIsActive = state;
    }
    
    public List<Node> SpawnPath()
    {
        stairs.gameObject.SetActive(false);
        var platform = FireRay();
        GameManager.instance.EnvironmentManager.PlatformManager.RaisePlatform(platform.gameObject);

      
      var t = GameManager.instance.pathfinding.ReturnPath();
      var l = GameManager.instance.EnvironmentManager.environmentSpawner.GetNode(t);
      GameManager.instance.EnvironmentManager.pathFinding.InitializeConnectingPath(platform.Node,l);
      foreach (var go in t)
        {
            GameManager.instance.EnvironmentManager.PlatformManager.RaisePlatform(go.platform);
        }
        stairs.gameObject.SetActive(true);
        return t;
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

    public void ActivateStairs()
    {
        stairs.gameObject.SetActive(true);
    }
    
    public Node Node;
    public void SetNode(Node node)
    {
        Node = node;
    }
    private PlatformState FireRay()
    {
        RaycastHit hit;
        Ray ray = default;
        float thickness = 1f; //<-- Desired thickness here.
        Vector3 origin = raycastHolder.transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        
        if (Physics.Raycast(origin, direction, out hit, 25f))
        {
            if(hit.collider.GetComponent<PlatformState>())
                return hit.collider.GetComponent<PlatformState>();
        }

        return null;
    }

    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
}


