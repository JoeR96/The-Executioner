using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Event : MonoBehaviour, IStartEvent
{
    [field: SerializeField] protected GameObject eventGameObject { get; set; }
    [SerializeField] protected EventManager eventManager;
    protected Transform eventTargetDestination{ get; private set; }
    protected GameObject activeEventGameObject;

    protected int targetKillCount;
    public int currentTargetKillCount  { get; private set; }
    
    private void Awake()
    {
        eventManager = GameManager.instance.EventManager;
    }
    public int progress { get; set; }
    public virtual void StartEvent( )
    {
        SetEventDestination();
        activeEventGameObject = gameObject;
        transform.position = eventTargetDestination.position;
        AddEventTransformsToMaster();
    }

    public void SetEventDestination()
    {
        eventTargetDestination = eventManager.ReturnAvailableEventLocation();
    }

    public void AddEventTransformsToMaster()
    {
        eventManager.AddEventDestinationToList(eventTargetDestination);
        eventManager.AddEventTransformObjectToList(activeEventGameObject);
    }

    private void OnTriggerEnter(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(true);
    }
    
    private void OnTriggerExit(Component other)
    {
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        enemy?.IsInArea(false);
    }

    public Event ReturnActiveEevent()
    {
        return this;
    }
}
