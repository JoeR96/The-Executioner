using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlatformHeight
{
    Raised,
    RaisedBetween,
    RaisedTwice,
    Underground,
    Flat,
    RaisedFour,
    RaisedSix,
    LoweredSix
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
    public GameObject[,] connectingPlatforms = new GameObject[2,2];
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
    private Vector3 startPosition;
    private Vector3 bridgeStartPosition;
    
    private void Start()
    {
        PlatformIsWall = true;
        PlatformIsActive = false;
        startPosition = transform.position;
        bridgeStartPosition = bridge.transform.position;
        CurrentHeight = (int)PlatformHeight.Flat;
    }
    
    public List<Node> SpawnPath()
    { 
        SetPlatformHeight((int)PlatformHeight.Raised);
        var t = GameManager.instance.pathfinding.ReturnPath();
      var l = GameManager.instance.EnvironmentManager.environmentSpawner.GetNode(t);
      GameManager.instance.EnvironmentManager.pathFinding.InitializeConnectingPath(Node,l);
      foreach (var go in t)
        {
            var state = go.platform.GetComponent<PlatformState>();
            if (!state.PlatformIsActive)
            {
                state.SetPlatformHeight((int) PlatformHeight.RaisedTwice);
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
        stairs.GetComponent<MeshRenderer>().enabled = active;
        stairs.GetComponent<MeshCollider>().enabled = active;
    }

    public void ActivateBridge(bool active)
    {
        bridge.GetComponentInChildren<MeshRenderer>().enabled = active;
        bridge.GetComponentInChildren<BoxCollider>().enabled = active;
    }

    public void SetStateFromExternal(int height, int stairState, bool stairActive,int platformHeight, bool platformBridgeActive,int currentColour,bool spawnPointActive)
    {
        CurrentColour = currentColour;
        PlatformBridgeActive = platformBridgeActive;
        PlatformStairActive = stairActive;
        CurrentHeight = height;
        CurrentRotation = stairState;
        PlatformSpawnPointActive = spawnPointActive;
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
    public void SetPlatformHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformHeight.Flat)
        {
            SetPosition(gameObject,-28,PlatformHeight.Flat,true);
        }
        if (height == (int)PlatformHeight.Raised)
        {
            SetPosition(gameObject,-6,PlatformHeight.Raised,true);
        }
        if (height == (int)PlatformHeight.RaisedBetween)
        {
            SetPosition(gameObject,-2,PlatformHeight.RaisedBetween,true);
        }
        if (height == (int)PlatformHeight.RaisedTwice)
        {
            SetPosition(gameObject,-10, PlatformHeight.RaisedTwice,true);
        }
        if (height == (int)PlatformHeight.Underground)
        {
            SetPosition(gameObject,2, PlatformHeight.Underground,true);
        }
        if (height == (int)PlatformHeight.RaisedFour)
        {
            SetPosition(gameObject,-28, PlatformHeight.RaisedFour,true);
        }
        if (height == (int)PlatformHeight.RaisedSix)
        {
            SetPosition(gameObject,-22, PlatformHeight.RaisedSix,true);
        }
        if (height == (int)PlatformHeight.LoweredSix)
        {
            SetPosition(gameObject,18, PlatformHeight.RaisedSix,true);
        }
        CurrentHeight = (int)height;
    }
    public void SetNegativePlatformHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformHeight.Flat)
        {
            SetPosition(gameObject,25,PlatformHeight.Flat,true);
        }
        if (height == (int)PlatformHeight.Raised)
        {
            SetPosition(gameObject,15,PlatformHeight.Raised,true);
        }
        if (height == (int)PlatformHeight.RaisedTwice)
        {
            SetPosition(gameObject,20, PlatformHeight.RaisedTwice,true);
        }
        if (height == (int)PlatformHeight.Underground)
        {
            SetPosition(gameObject,-0, PlatformHeight.Underground,true);
        }
        if (height == (int)PlatformHeight.RaisedFour)
        {
            SetPosition(gameObject,26, PlatformHeight.RaisedFour,true);
        }
        if (height == (int)PlatformHeight.RaisedSix)
        {
            SetPosition(gameObject,-40, PlatformHeight.RaisedSix,true);
        }
        CurrentHeight = (int)height;
    }

    public void SetBridgeHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformBridgeHeight.LowBridge)
        {
            SetBridgePosition(bridge,-35,PlatformBridgeHeight.LowBridge,false);
        }
        if (height == (int)PlatformBridgeHeight.Middlebridge)
        {
            SetBridgePosition(bridge,-15,PlatformBridgeHeight.Middlebridge,false);
        }
        if (height == (int)PlatformBridgeHeight.HighBridge)
        {
            SetBridgePosition(bridge,-20, PlatformBridgeHeight.HighBridge,false);
        }


        CurrentBridgeHeight = (int)height;
    }
    private void SetBridgePosition(GameObject go,float targetHeight,PlatformBridgeHeight state,bool isPlatform)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, startPosition.y - targetHeight, go.transform.position.z);
        if (state == PlatformBridgeHeight.Disabled)
        {
            targetPosition = startPosition;
        }
        CurrentBridgeHeight = (int) state;
        StartCoroutine(LerpPosition(go,targetPosition, 0.25f));
        
        
        
    }
    private void SetPosition(GameObject go,float targetHeight,PlatformHeight state,bool isPlatform)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, startPosition.y - targetHeight, go.transform.position.z);
        if (state == PlatformHeight.Flat)
        {
            targetPosition = startPosition;
        }
        StartCoroutine(LerpPosition(go,targetPosition, 1f));
        if (isPlatform)
        {
            CurrentHeight = (int)state;
        }
        else
        {
            CurrentBridgeHeight = (int) state;
        }
        
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
        var adjacent = GameManager.instance.EnvironmentManager.environmentSpawner.CheckAdjacentPositions(Node);
        foreach (var go in adjacent)
        {
           go.platform.GetComponent<MeshRenderer>().material = materials[materialIndex];
            go.PlatformState.CurrentColour = materialIndex;
            go.PlatformState.
            //go.PlatformState.ChangeMaterial(materialIndex);
            CurrentColour = materialIndex;
        }
    }

    // private void ChangeMaterial(int materialIndex)
    // {
    //     StartCoroutine(LerpMaterial(materialIndex));
    // }
    public void SetPlatformColour(int materialIndex)
    {
        GetComponent<MeshRenderer>().material = materials[materialIndex];
    }
}


