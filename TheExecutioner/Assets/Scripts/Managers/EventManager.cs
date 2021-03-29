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

    private List<Transform> activeEvents = new List<Transform>();
   

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
            }
        }
        foreach (var go in zombieSpawner.ArmoredZombies)
        {
            var x = go.GetComponent<AiAgent>();
            x.StateMachine.ChangeState(StateId.EventState);
        }
    }
    public Transform ReturnActiveRandomEventLocation()
    {
        var random = Random.Range(0, activeEvents.Count);
        var _ = activeEvents[random];
        activeEvents.RemoveAt(random);
        return _;
    }

    public Transform ReturnAvailableEventLocation()
    {
        var eventLocation = enemySpawnPoints.ReturnEventSpawnPoint();
        return eventLocation;
    }

    
    public void PlaySacrificeEvent()
    {
        var eventLocation = ReturnAvailableEventLocation();
        SacrificeEvent.StartEvent(eventLocation);
        activeEvents.Add(eventLocation);
    }


    public void PlayHeartEscortEvent()
    {
        var eventLocation = ReturnAvailableEventLocation();
        HeartEscortEvent.StartEvent(eventLocation);
        activeEvents.Add(eventLocation);
    }

}
