using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Event : MonoBehaviour, IStartEvent
{
    [field: SerializeField] protected GameObject eventGameObject { get; set; }
    [SerializeField] protected EventManager eventManager;
    protected Transform eventTargetDestination{ get; private set; }
    protected GameObject activeEventGameObject;

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
}
