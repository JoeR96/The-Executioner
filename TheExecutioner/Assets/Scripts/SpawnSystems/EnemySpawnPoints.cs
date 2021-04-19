using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProGrids;


public class EnemySpawnPoints : MonoBehaviour
{
     [SerializeField] List<Transform> externalSpawnPoints = new List<Transform>();
     [SerializeField] List<Transform> internalSpawnPoints = new List<Transform>();
     [SerializeField] List<Transform> eventSpawnPositions = new List<Transform>();

     public List<Transform> ExternalSpawnPoints => externalSpawnPoints;
     public List<Transform> InternalSpawnPoints => internalSpawnPoints;
     public List<Transform> EventSpawnPoints => eventSpawnPositions;
     

    public void AddExternalSpawnPointToList(Transform spawn)
    {
        externalSpawnPoints.Add(spawn);
    }
    public void AddInternalSpawnPointToList(Transform spawn)
    {
        internalSpawnPoints.Add(spawn);
    }
    
    public void CheckForSpawnPoint(Node node)
    {
        if(node.PlatformManager.PlatformSpawnManager.PlatformSpawnPointActive)
            AddInternalSpawnPointToList(node.PlatformManager.PlatformSpawnManager.spawnPoint.transform);
    }
    
    public Transform ReturnInternalSpawnPoint()
    {
        var random = Random.Range(0, internalSpawnPoints.Count);
        return internalSpawnPoints[random];
    }
    
    public Transform ReturnExternalSpawnPoint()
    {
        var random = Random.Range(0, internalSpawnPoints.Count);
        return internalSpawnPoints[random];
    }

    public Transform ReturnEventSpawnPoint()
    {
        Debug.Log(eventSpawnPositions.Count);
        var random = Random.Range(0,eventSpawnPositions.Count);
        var temp = eventSpawnPositions[random];
        
        eventSpawnPositions.RemoveAt(random);
        
        return temp;
    }
    

    public void ClearEventSpawns()
    {
        eventSpawnPositions.Clear();
    }

    public void AddEventSpawn(Transform spawn)
    {
        eventSpawnPositions.Add(spawn);
    }
}
