using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortTest : MonoBehaviour
{
    private RandomPointOnNavMesh randomPointOnNavMesh;
    
    
    // Start is called before the first frame update
    void Start()
    {
        randomPointOnNavMesh = GetComponent<RandomPointOnNavMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
