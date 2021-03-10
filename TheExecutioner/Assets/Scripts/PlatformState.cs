using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlatformHeight
{
    Raised,
    RaisedTwice,
    RaisedThree,
    RaisedHalf,
    RaisedTwiceHalf,
    RaisedThreeHalf,
    Underground,
    UndergroundHalf,
    Flat,
    RaisedFour,
    HighArenaWall,
    LoweredArenaWall
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
    [SerializeField] public GameObject halfStairs;
    [SerializeField] public GameObject bridge;
    [SerializeField] public GameObject spawnPoint;
    
    [SerializeField] private List<float> rotations = new List<float>();
    [SerializeField] private Material[] materials; 
    
    public Node Node;
    
    #region platform settings
    public bool ColourTileMode;
    public bool ColourAdjacentMode;
    public bool PlatformStairActive;
    public bool PlatformHalfStairActive;
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
    #endregion
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
    
    public void ActivateStairs(bool active)
    {
        stairs.GetComponent<MeshRenderer>().enabled = active;
        stairs.GetComponent<MeshCollider>().enabled = active;
    }
    public void ActivateHalfStairs(bool active)
    {
        halfStairs.GetComponent<MeshRenderer>().enabled = active;
        halfStairs.GetComponent<MeshCollider>().enabled = active;
    }
    public void ActivateBridge(bool active)
    {
        bridge.GetComponentInChildren<MeshRenderer>().enabled = active;
        bridge.GetComponentInChildren<BoxCollider>().enabled = active;
    }

    public void SetStateFromExternal(int height, int stairState, bool stairActive,int platformHeight, bool platformBridgeActive,int currentColour)
    {
        CurrentColour = currentColour;
        PlatformBridgeActive = platformBridgeActive;
        PlatformStairActive = stairActive;
        CurrentHeight = height;
        CurrentRotation = stairState;
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
    
    public bool ReturnStairValue()
    {
        PlatformStairActive = !PlatformStairActive;
        return PlatformStairActive;
    }
    public bool ReturnHalfStairValue()
    {
        PlatformHalfStairActive = !PlatformStairActive;
        return PlatformHalfStairActive;
    }
    public bool ReturnBridgeValue()
    {
        PlatformBridgeActive = !PlatformBridgeActive;
        return PlatformBridgeActive;
    }
    
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
            SetPosition(gameObject,-8f);
        
        if (height == (int)PlatformHeight.RaisedHalf)
            SetPosition(gameObject,-6.325f);
        
        if (height == (int)PlatformHeight.Raised)
            SetPosition(gameObject,-4.65f);
        
        if (height == (int)PlatformHeight.RaisedTwiceHalf)
            SetPosition(gameObject,-2.975f);
        
        if (height == (int)PlatformHeight.RaisedTwice)
            SetPosition(gameObject,-1.3f);
        
        if (height == (int)PlatformHeight.RaisedThreeHalf)
            SetPosition(gameObject,0.375f);
        
        if (height == (int)PlatformHeight.RaisedThree)
            SetPosition(gameObject,2.05f);
        
        if (height == (int)PlatformHeight.UndergroundHalf)
            SetPosition(gameObject,-9.675f);
        
        if (height == (int)PlatformHeight.Underground)
            SetPosition(gameObject,-11.35f);
        
        if (height == (int)PlatformHeight.RaisedFour)
            SetPosition(gameObject,-24.5f);
        
        if (height == (int)PlatformHeight.HighArenaWall)
            SetPosition(gameObject,-19.5f);
        if (height == (int)PlatformHeight.LoweredArenaWall)
            SetPosition(gameObject,28.5f);
        
        
        CurrentHeight = height;
    }
    public void SetNegativePlatformHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformHeight.Flat)
        {
            SetPosition(gameObject,25);
        }
        if (height == (int)PlatformHeight.Raised)
        {
            SetPosition(gameObject,15);
        }
        if (height == (int)PlatformHeight.RaisedTwice)
        {
            SetPosition(gameObject,20);
        }
        if (height == (int)PlatformHeight.Underground)
        {
            SetPosition(gameObject,-5);
        }
        if (height == (int)PlatformHeight.RaisedFour)
        {
            SetPosition(gameObject,26);
        }
        if (height == (int)PlatformHeight.HighArenaWall)
        {
            SetPosition(gameObject,-40);
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
    private void SetPosition(GameObject go,float targetHeight)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, startPosition.y - targetHeight, go.transform.position.z);

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

        go.transform.position = targetPosition;

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
        stairs.GetComponent<MeshRenderer>().material = materials[materialIndex];
    }
}


