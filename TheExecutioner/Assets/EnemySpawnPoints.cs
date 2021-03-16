using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProGrids;

public class EnemySpawnPoints : MonoBehaviour
{
    public List<Transform> externalSpawnPoints = new List<Transform>();
    public List<Transform> internalSpawnPoints = new List<Transform>();
    
    public List<Transform> ReturnExternalSpawnPoints()
    {
        return externalSpawnPoints;
    }

    public List<Transform> ReturnInternalSpawnPoints()
    {
        return internalSpawnPoints;
    }
    public void AddExternalSpawnPointToList(Node node)
    {
        externalSpawnPoints.Add(node.PlatformManager.PlatformSpawnManager.spawnPoint.transform);
    }
    public void AddInternalSpawnPointToList(Node node)
    {
        internalSpawnPoints.Add(node.PlatformManager.PlatformSpawnManager.spawnPoint.transform);
    }
    
    public void CheckForSpawnPoint(Node node)
    {
        if(node.PlatformManager.PlatformSpawnManager.PlatformSpawnPointActive)
            AddInternalSpawnPointToList(node);
    }
    
}
