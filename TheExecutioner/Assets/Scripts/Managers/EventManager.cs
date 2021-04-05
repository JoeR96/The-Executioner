using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    [SerializeField] 
    private ZombieSpawner zombieSpawner;
    
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;
    
    public SacrificeEvent SacrificeEvent { get; private set; }
    public HeartEscortEvent HeartEscortEvent { get; private set; }

    private List<Transform> activeEventTargetDestinations = new List<Transform>();
    [SerializeField] private List<GameObject> activeEventGameObjects = new List<GameObject>();
   

    private void Start()
    {
        SacrificeEvent = GetComponent<SacrificeEvent>();
        HeartEscortEvent = GetComponent<HeartEscortEvent>();
    }

    public void AssignEvents()
    {
        foreach (var go in zombieSpawner.ActiveFodderZombies)
        {
            if (go.GetComponent<AiAgent>() != null)
            {
                var x = go.GetComponent<AiAgent>();
                x.StateMachine.ChangeState(StateId.EventState);
                Debug.Log(x.StateMachine);
            }
        }
        foreach (var go in zombieSpawner.ArmoredZombies)
        {
            if (go.GetComponent<AiAgent>() != null)
            {
                var x = go.GetComponent<AiAgent>();
                x.StateMachine.ChangeState(StateId.EventState);
                Debug.Log(x.StateMachine);
            }
        }
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
        SacrificeEvent.StartEvent();
    }


    public void PlayHeartEscortEvent()
    {
        HeartEscortEvent.StartEvent();
    }

}
