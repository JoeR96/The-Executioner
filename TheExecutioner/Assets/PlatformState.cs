using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public class PlatformState : MonoBehaviour
{
    [SerializeField] private GameObject _raycastHolder;
    
    public int X;
    public int Z;
    
    private bool platformInUse;
    

    public void SetState()
    {
        platformInUse = !platformInUse;
    }

    public bool ReturnState()
    {
        return platformInUse;
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
    
    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
}


