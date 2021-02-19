using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCheck : MonoBehaviour
{
    [SerializeField] private GameObject raycastHolder;
    // Start is called before the first frame update
    public void InvokeStairs()
    {
        Invoke("CheckDistance",0.5f);
    }

    private void CheckDistance()
    {
        Ray raycast; 
        RaycastHit rayHit;
        Vector3 origin = raycastHolder.transform.position;
        Vector3 direction = -raycastHolder.transform.up;

        if (Physics.Raycast(origin, direction, out rayHit, 45f))
        {
            Debug.Log(origin);
            Debug.Log(rayHit.collider.transform.position);
            transform.position = rayHit.point;
            Debug.Log(transform.position);
            Debug.Log(rayHit.collider.transform.position);
        }

    }
}
