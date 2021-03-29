using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EscortTest : MonoBehaviour
{
    private RandomPointOnNavMesh randomPointOnNavMesh;
    [SerializeField] private AiAgent agent;
    private Vector3 targetPos;

    [SerializeField] private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
        randomPointOnNavMesh = GetComponent<RandomPointOnNavMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            
            var t = randomPointOnNavMesh.RandomPoint(transform.position,45f,out targetPos);
            if (t)
            {
                var go = Instantiate(cube, targetPos, quaternion.identity);
                agent.Player = go.transform;
            }
                

        }
    }
}
