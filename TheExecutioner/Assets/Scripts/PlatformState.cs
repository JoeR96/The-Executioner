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
    Underground,
    Flat,
    RaisedFour
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
    public bool PlatformStairActive;
    public bool PlatformBridgeActive;
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
            go.platform.GetComponent<PlatformState>().SetPlatformHeight((int)PlatformHeight.Raised);
        }
        stairs.gameObject.SetActive(true);
        return t;
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

    private void SetPlatformColour(int index)
    {
        var t = GetComponent<MeshRenderer>();
        t.material = GameManager.instance.EnvironmentManager.environmentSpawner.Materials[index];
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

    public void SetPlatformHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformHeight.Flat)
        {
            SetPosition(gameObject,-25,PlatformHeight.Flat,true);
        }
        if (height == (int)PlatformHeight.Raised)
        {
            SetPosition(gameObject,-5,PlatformHeight.Raised,true);
        }
        if (height == (int)PlatformHeight.RaisedTwice)
        {
            SetPosition(gameObject,-10, PlatformHeight.RaisedTwice,true);
        }
        if (height == (int)PlatformHeight.Underground)
        {
            SetPosition(gameObject,5, PlatformHeight.Underground,true);
        }
        if (height == (int)PlatformHeight.RaisedFour)
        {
            SetPosition(gameObject,-16, PlatformHeight.Underground,true);
        }
        CurrentHeight = (int)height;
    }

    public void SetBridgeHeight(int height)
    {
        Vector3 targetPosition;
        if (height == (int)PlatformBridgeHeight.LowBridge)
        {
            SetPosition(bridge,-25,PlatformHeight.Flat,false);
        }
        if (height == (int)PlatformBridgeHeight.Middlebridge)
        {
            SetPosition(bridge,-5,PlatformHeight.Raised,false);
        }
        if (height == (int)PlatformBridgeHeight.HighBridge)
        {
            SetPosition(bridge,-10, PlatformHeight.RaisedTwice,false);
        }


        CurrentBridgeHeight = (int)height;
    }
    
    private void SetPosition(GameObject go,float targetHeight,PlatformHeight state,bool isPlatform)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(go.transform.position.x, startPosition.y - targetHeight, go.transform.position.z);
        if (state == PlatformHeight.Flat)
        {
            targetPosition = startPosition;
        }
        StartCoroutine(LerpPosition(go,targetPosition, 0.25f));
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

    
    public void SetStairRotation( int stairState)
    {
        stairs.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                transform.localRotation.eulerAngles.y + rotations[stairState],
                transform.localRotation.eulerAngles.z);
            CurrentRotation = stairState;
    }
    
}


