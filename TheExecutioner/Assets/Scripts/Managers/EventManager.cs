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
    [SerializeField] private List<Transform> activeEventTransforms = new List<Transform>();
   

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
        var random = Random.Range(0, activeEventTransforms.Count);
        var _ = activeEventTransforms[random];
        activeEventTransforms.RemoveAt(random);
        return _;
    }
    
    public Transform ReturnActiveRandomEventTransform()
    {
        var random = Random.Range(0, activeEventTransforms.Count);
        var _ = activeEventTransforms[random];
        return _;
    }

    public Transform ReturnAvailableEventLocation()
    {
        return enemySpawnPoints.ReturnEventSpawnPoint();
    }


    public void AddEventDestinationToList(Transform destination)
    {
        activeEventTargetDestinations.Add(destination);
    }

    public void AddEventTransformObjectToList(Transform eventTransform)
    {
        activeEventTransforms.Add(eventTransform);
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
