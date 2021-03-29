using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    [SerializeField] 
    private ZombieSpawner zombieSpawner;
    
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;
    
    private SacrificeEvent sacrificeEvent;
    private HeartEscortEvent heartEscortEvent;
    private List<Transform> activeEvents = new List<Transform>();
    
    private void Start()
    {
        sacrificeEvent = GetComponent<SacrificeEvent>();
        heartEscortEvent = GetComponent<HeartEscortEvent>();
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
        return activeEvents[random];
    }

    private Transform ReturnAvailableEventLocation()
    {
        var eventLocation = enemySpawnPoints.ReturnEventSpawnPoint();
        Debug.Log(eventLocation);
        return eventLocation;
    }

    
    public void PlaySacrificeEvent()
    {
        var eventLocation = ReturnAvailableEventLocation();
        sacrificeEvent.StartEvent(eventLocation);
        activeEvents.Add(eventLocation);
    }


    public void PlayHeartEscortEvent()
    {
        var eventLocation = ReturnAvailableEventLocation();
        heartEscortEvent.StartEvent(eventLocation);
        activeEvents.Add(eventLocation);
    }

}
