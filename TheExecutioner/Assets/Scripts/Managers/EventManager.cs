using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    [SerializeField] 
    private ZombieSpawner zombieSpawner;
    
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;

    [SerializeField] 
    private GameObject SacrificeEvent;

    [SerializeField]
    private GameObject HeartEscortEvent;

    private List<Transform> activeEventTargetDestinations = new List<Transform>();
    [SerializeField] private List<GameObject> activeEventGameObjects = new List<GameObject>();
   

    private void Start()
    {
       
    }


    public Transform ReturnActiveRandomEventLocation()
    {
        var random = Random.Range(0, activeEventGameObjects.Count);
        var _ = activeEventGameObjects[random];
        activeEventGameObjects.RemoveAt(random);
        return _.transform;
    }
    
    public GameObject ReturnActiveRandomEventTransform()
    {
        var random = Random.Range(0, activeEventGameObjects.Count);
        var _ = activeEventGameObjects[random];
        return _.gameObject;
    }

    public Transform ReturnAvailableEventLocation()
    {
        return enemySpawnPoints.ReturnEventSpawnPoint();
    }


    public void AddEventDestinationToList(Transform destination)
    {
        activeEventTargetDestinations.Add(destination);
    }

    public void AddEventTransformObjectToList(GameObject go)
    {
        activeEventGameObjects.Add(go);
    }


    public void PlaySacrificeEvent()
    {
        Instantiate(SacrificeEvent);
    }


    public void PlayHeartEscortEvent()
    {
        Instantiate(HeartEscortEvent);
    }

}
