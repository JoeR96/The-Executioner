using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Event : MonoBehaviour, IStartEvent, IReturnEvent
{
    [field: SerializeField] protected GameObject eventGameObject { get; set; }
    [SerializeField] protected EventManager eventManager;
    protected Transform eventTargetDestination{ get; private set; }
    protected GameObject activeEventGameObject;

    public int progress { get; set; }
    public virtual void StartEvent( )
    {
        SetEventDestination();
        activeEventGameObject = Instantiate(eventGameObject, eventTargetDestination.position, quaternion.identity);
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
