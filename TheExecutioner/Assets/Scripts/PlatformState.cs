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
    Flat
}

public enum PlatformStairState
{
    Zero,
    One,
    Two,
    Three,

    
}

public class PlatformState : MonoBehaviour
{

    [SerializeField] public GameObject stairs;
    [SerializeField] private GameObject raycastHolder;
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] private int boundarySize;
    [SerializeField] private List<float> rotations = new List<float>();
    public GameObject[,] connectingPlatforms = new GameObject[2,2];
    public bool PlatformStairActive;
    public bool PlatformIsPlatform = true;
    public bool PlatformIsActive = false;
    public int X;
    public int Z;
    public int CurrentHeight;
    public int CurrentRotation;
    private Vector3 startPosition;

    
    private void Start()
    {
        PlatformIsActive = false;
        startPosition = transform.position;
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

    public void SetStateFromExternal(int height, int stairState, bool stairActive)
    {
        PlatformStairActive = stairActive;
        CurrentHeight = height;
        CurrentRotation = stairState;
        SetState();
    }
    public void SetState()
    {
        SetPlatformHeight(CurrentHeight);
        SetStairRotation(CurrentRotation);
        if(PlatformStairActive)
        {
            ActivateStairs(true);
        }
        
    }
    
    
    public bool ReturnStairValue()
    {
        PlatformStairActive = !PlatformStairActive;
        return PlatformStairActive;
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
            SetPosition(-25,PlatformHeight.Flat);
        }
        if (height == (int)PlatformHeight.Raised)
        {
            SetPosition(-5,PlatformHeight.Raised);
        }
        if (height == (int)PlatformHeight.RaisedTwice)
        {
            SetPosition(-10, PlatformHeight.RaisedTwice);
        }
        if (height == (int)PlatformHeight.Underground)
        {
            SetPosition(5, PlatformHeight.Underground);
        }

        CurrentHeight = (int)height;
    }

    private void SetPosition(float targetHeight,PlatformHeight state)
    {
        Vector3 targetPosition;
        targetPosition = new Vector3(transform.position.x, startPosition.y - targetHeight, transform.position.z);
        if (state == PlatformHeight.Flat)
        {
            targetPosition = startPosition;
        }
        StartCoroutine(LerpPosition(targetPosition, 0.25f));
        CurrentHeight = (int)state;
 
    }

    private IEnumerator LerpPosition( Vector3 targetPosition, float duration)
    {
        Transform startPosition = transform;
        float timer = 0f;
        float _duration = duration;
         
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            transform.position = Vector3.Lerp(startPosition.position,  targetPosition, percentage);
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


