using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Transform SpawnPoint;
    public int gridX;
    public int gridZ;
    public int y;
    public float gridSpaceOffset;
    public GameObject floorContainer;

    protected void Start()
    {
        SpawnGrid();
    }
    protected void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpaceOffset,y, z * gridSpaceOffset );
                SpawnChunk(spawnPosition,quaternion.identity);
            }
        }
    }

    protected void SpawnChunk(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        var chunk = Instantiate(floorContainer, positionToSpawn, rotationToSpawn);
        chunk.transform.SetParent(gameObject.transform);
        
    }

    public IEnumerator LerpTransformPosition(Transform transformToLerp, Vector3 target, float duration)
    {
        Transform startPosition = transformToLerp;
        float timer = 0f;
        float _duration = duration;
        while (timer < _duration)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / _duration, 1);
            Debug.Log(transformToLerp);
            transformToLerp.position = Vector3.Lerp(startPosition.position, target, percentage);
            yield return null;
        }
    }
}


