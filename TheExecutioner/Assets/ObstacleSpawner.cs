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
  

    

}
