using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _raycastHolder;
    private int pathLength = 15;
    private EnvironmentManager environmentManager;
    // Start is called before the first frame update
    private void Awake()
    {
        environmentManager = GetComponent<EnvironmentManager>();
    }
    
    void Start()
    {
        //StartCoroutine(RaisePlatform(pathLength));
    }


    private GameObject FireRay()
    {
        RaycastHit hit;
        float thickness = 1f; //<-- Desired thickness here.
        Vector3 origin = _raycastHolder.transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        
        if (Physics.SphereCast(origin, thickness, direction, out hit))
        {
            return hit.collider.gameObject;
        }

        return null;
    }

    private float _distanceBetweenPoints = 2f;
    
    private void SetPosition(int x, int z)
    {
        var tiles = environmentManager.tileArray;
        var position = tiles[x,z].transform.position;
        var offsetPosition =  new Vector3(position.x, 45f, position.z);
         _raycastHolder.transform.position = offsetPosition;
    }


    
    
    private IEnumerator RaisePlatform(int pathLength)
    {
        for (int i = 0; i < pathLength; i++)
        {
        
            var platform = FireRay();
            var platformState = platform.GetComponent<PlatformState>();

            if (!platformState) 
            {
                yield break;
            }
            
            if (platformState.ReturnState())
            {
                yield return null;
            }

            var coords = ReturnPosition();
            var x = coords.Item1;
            var z = coords.Item2;
            
            SetPosition(x,z);
            RaisePlatforms(platform.transform);
            platformState.SetState();
            yield return new WaitForSeconds(0.125f);
        
        }
    }

    private Tuple<int,int> ReturnPosition()
    {
        int x  = Random.Range(0,15);
        int z = Random.Range(0,15); ;
        return new Tuple<int, int>(x, z);
    }
    
    private void RaisePlatforms(Transform currentPosition)
    {
        Vector3 targetPosition = currentPosition.position;
        targetPosition.y = targetPosition.y += 5f;
        StartCoroutine(LerpTransformPosition(currentPosition,targetPosition, 0.5f));
    }
    
    public IEnumerator LerpTransformPosition(Transform transformToLerp, Vector3 target, float duration)
    {
        Transform startPosition = transformToLerp;
        float timer = 0f;
        float _duration = duration;
        
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            transformToLerp.position = Vector3.Lerp(startPosition.position, target, percentage);
            yield return null;
        }
    }
}
