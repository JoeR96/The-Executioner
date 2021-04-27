using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent, IReturnEvent, IDisplayEventText
{
    [field: SerializeField] protected GameObject eventGameObject { get; set; }
    [SerializeField] protected EventManager eventManager;
    [field: SerializeField] protected Transform eventTargetDestination{ get; private set; }
    protected GameObject activeEventGameObject;
    protected EventZombieSpawner eventZombieSpawner;
    public int EventTargetKillCount { get; set; }
    public EventTargetKillCount EventTargetKillCountManager;
    protected int waveSpawnTotal;
    public int currentTargetKillCount;
    
    private void Awake()
    {
        eventManager = GameManager.instance.EventManager;
        EventTargetKillCountManager = new EventTargetKillCount(EventTargetKillCount);
    }

    private void Update()
    {
        currentTargetKillCount = EventTargetKillCountManager.CurrentKillCount;
    }
    public int progress { get; set; }
    public virtual void StartEvent( )
    {
        SetEventDestination();
        activeEventGameObject = gameObject;
        transform.position = eventTargetDestination.position;
        AddEventTransformsToMaster();
        AddEventToList();
        eventZombieSpawner = new EventZombieSpawner(waveSpawnTotal,transform);
        eventZombieSpawner.SpawnZombiesTargetingEvent();
    }

    private void AddEventToList()
    {
        eventManager.AddEvent(this);
    }

    public void SetEventDestination()
    {
        eventTargetDestination = eventManager.ReturnAvailableEventLocation();
    }

    public void AddEventTransformsToMaster()
    {
        eventManager.AddEventDestinationToList(eventTargetDestination);
        eventManager.AddEventTransformObjectToList(activeEventGameObject);
        Debug.Log("Event should add");
    }

    public virtual void OnTriggerEnter(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(true);
    }
    
    private void OnTriggerExit(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(false);
    }

    public Event ReturnActiveEvent()
    {
        return this;
    }
}
