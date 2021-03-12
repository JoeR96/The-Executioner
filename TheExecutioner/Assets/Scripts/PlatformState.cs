using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlatformHeight
{
    Flat,
    LevelOne,
    LevelTwo,
    LevelThree,
    LevelFour,
    LevelFive,
    LevelSix,
    UndergroundOne,
    UndergroundTwo,
    RaisedOuterWall,
    LoweredOuterWall
}

public enum PlatformBridgeHeight
{
    LowBridge,
    Middlebridge,
    HighBridge,
    Disabled

    
}

public class PlatformState : MonoBehaviour
{

    [SerializeField] public GameObject stairs;
    [SerializeField] public GameObject bridge;
    [SerializeField] private GameObject raycastHolder;
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] private int boundarySize;
    [SerializeField] private List<float> rotations = new List<float>();
    [SerializeField] private List<int> platformHeights = new List<int>();
    #region platform properties
    public Material[] materials; 
    public bool ColourTileMode;
    public bool ColourAdjacentMode;
    public bool PlatformStairActive;
    public bool PlatformSpawnPointActive;
    public bool PlatformBridgeActive;
    public bool PlatformIsWall;
    public bool PlatformIsPlatform = true;
    public bool PlatformIsActive = false;
    public int X;
    public int Z;
    public int CurrentColour;
    public int CurrentHeight;
    public int CurrentBridgeHeight;
    public int CurrentRotation;
    #endregion
    private Vector3 startPosition;

    
    private void Start()
    {
        PlatformIsWall = true;
        PlatformIsActive = false;
        startPosition = transform.position;
        CurrentHeight = (int)PlatformHeight.Flat;
    }
    
    public List<Node> SpawnPath()
    { 
        SetPlatformHeight((int)PlatformHeight.LevelOne);
        var t = GameManager.instance.pathfinding.ReturnPath();
      var l = GameManager.instance.EnvironmentManager.environmentSpawner.GetNode(t);
      GameManager.instance.EnvironmentManager.pathFinding.InitializeConnectingPath(Node,l);
      foreach (var go in t)
        {
            var state = go.platform.GetComponent<PlatformState>();
            if (!state.PlatformIsActive)
            {
                state.SetPlatformHeight((int) PlatformHeight.LevelTwo);
            }
            else
                state.SetBridgeHeight((int)PlatformBridgeHeight.HighBridge);
            
        }
        stairs.gameObject.SetActive(true);
        return t;
    }

    public void ActivateSpawnPoint(bool active)
    {
        
    }
    public void ActivateStairs(bool active)
    {
        stairs.GetComponentInChildren<MeshRenderer>().enabled = active;
        stairs.GetComponentInChildren<MeshCollider>().enabled = active;
    }

    public void ActivateBridge(bool active)
    {
        bridge.GetComponent<MeshRenderer>().enabled = active;
        bridge.GetComponent<BoxCollider>().enabled = active;
    }

    public void SetStateFromExternal(PlatformInformation platformInformation)
    {
        CurrentColour = platformInformation.CurrentBridgeHeight;
        PlatformBridgeActive = platformInformation.BridgeIsActive;
        PlatformStairActive = platformInformation.PlatformStairActive;
        CurrentHeight = platformInformation.CurrentHeight;
        CurrentRotation = platformInformation.CurrentRotation;
        PlatformSpawnPointActive = platformInformation.PlatformSpawnActive;
        SetState();
    }
    public void SetState()
    {
        
        SetPlatformHeight(CurrentHeight);
        SetStairRotation(CurrentRotation);
        SetPlatformColour(CurrentColour);
        ActivateStairs(PlatformStairActive);
        ActivateBridge(PlatformBridgeActive);
        SetBridgeHeight(CurrentBridgeHeight);
        ActivateSpawnPoint(PlatformSpawnPointActive);

    }

    public void ResetState()
    {
        PlatformStairActive = false;
        PlatformBridgeActive = false;
        
        SetPlatformHeight((int) PlatformHeight.Flat);
        SetBridgeHeight((int) PlatformBridgeHeight.Disabled);
        
        SetStairRotation(CurrentRotation);
        //SetPlatformColour(CurrentColour);
        ActivateStairs(PlatformStairActive);
        ActivateBridge(PlatformBridgeActive);
        
    }
    public bool ReturnPlatformSpawnPointValue()
    {
        PlatformSpawnPointActive = !PlatformSpawnPointActive;
        return PlatformSpawnPointActive;
    }
    public bool ReturnStairValue()
    {
        PlatformStairActive = !PlatformStairActive;
        return PlatformStairActive;
    }
    public bool ReturnBridgeValue()
    {
        PlatformBridgeActive = !PlatformBridgeActive;
        return PlatformBridgeActive;
    }
    public Node Node;
    public void SetNode(Node node)
    {
        Node = node;
    }
    
    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
    public void SetPlatformHeight(int height )
    {
        SetPosition(gameObject,platformHeights[height]);
        CurrentHeight = height;
    }
    

    public void SetBridgeHeight(int height)
    {
        Vector3 targetPosition;
        if (height == 0)
        {
            SetBridgePosition(bridge,-0.2f,PlatformBridgeHeight.LowBridge,false);
        }
        if (height == 1)
        {
            SetBridgePosition(bridge,3.8f,PlatformBridgeHeight.Middlebridge,false);
        }
        if (height == 2)
        {
            SetBridgePosition(bridge,7.8f, PlatformBridgeHeight.HighBridge,false);
        }


        CurrentBridgeHeight = (int)height;
    }
    private void SetBridgePosition(GameObject go,float targetHeight,PlatformBridgeHeight state,bool isPlatform)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, transform.position.y + 18f + targetHeight, go.transform.position.z);
        if (state == PlatformBridgeHeight.Disabled)
        {
            targetPosition = startPosition;
        }
        CurrentBridgeHeight = (int) state;
        StartCoroutine(LerpPosition(go,targetPosition, 0.25f));
        
        
        
    }
    private void SetPosition(GameObject go,float targetHeight)
    {
      
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, targetHeight - 10f, go.transform.position.z);
        Debug.Log(targetHeight);
        StartCoroutine(LerpPosition(go,targetPosition, 1f));
    }

    private IEnumerator LerpPosition( GameObject go,Vector3 targetPosition, float duration)
    {
        Transform startPosition = go.transform;
        float timer = 0f;
        float _duration = duration;
         
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            go.transform.position = Vector3.Lerp(startPosition.position,  targetPosition, percentage);
            yield return null;
        }
        
    }
    private IEnumerator LerpMaterial(int materialIndex)
    {
        Material targetMaterial = materials[materialIndex];
        Material startMaterial = GetComponent<MeshRenderer>().material;
        
        float timer = 0f;
        float _duration = 25f;
         
        while (timer < _duration)
        {
            
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            startMaterial.Lerp(startMaterial,targetMaterial,percentage);
            yield return null;
        }

    }
    public void SetStairRotation( int stairState)
    {
        stairs.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y + rotations[stairState],
                transform.localRotation.eulerAngles.z);
            CurrentRotation = stairState;
    }
    
    //editor tests
    public void SetAdjacentColour(int materialIndex)
    {
        var adjacent = GameManager.instance.EnvironmentManager.environmentSpawner.CheckAdjacentClosePositions(Node);
        foreach (var go in adjacent)
        {
           go.platform.GetComponent<MeshRenderer>().material = materials[materialIndex];
            go.PlatformState.CurrentColour = materialIndex;
            go.PlatformState.
            //go.PlatformState.ChangeMaterial(materialIndex);
            CurrentColour = materialIndex;
        }
    }
    
    public void SetPlatformColour(int materialIndex)
    {
        GetComponent<MeshRenderer>().material = materials[materialIndex];
    }
}


