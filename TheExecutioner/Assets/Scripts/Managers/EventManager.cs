using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;
    [SerializeField] 
    private GameObject SacrificeEvent;
    [SerializeField]
    private GameObject HeartEscortEvent;

    private List<Transform> activeEventTargetDestinations = new List<Transform>();
    [SerializeField] private List<GameObject> activeEventGameObjects = new List<GameObject>();
    public List<Event> ActiveEvents { get; } = new List<Event>();
    /// <summary>
    /// Return an available spawn point for an event
    /// </summary>
    /// <returns></returns>
    public Transform ReturnAvailableEventLocation()
    {
        return enemySpawnPoints.ReturnEventSpawnPoint();
    }
    /// <summary>
    /// Add an event destination to the list
    /// </summary>
    /// <param name="destination"></param>
    public void AddEventDestinationToList(Transform destination)
    {
        activeEventTargetDestinations.Add(destination);
    }

    public void AddEventTransformObjectToList(GameObject go)
    {
        activeEventGameObjects.Add(go);
    }
    /// <summary>
    /// Play a random event
    /// </summary>
    public void PlayEvent()
    {
        if (Random.value < 0.5)
        {
            PlaySacrificeEvent();
            Debug.Log("Sacr");
        }
        else
        {
            PlayHeartEscortEvent();
            Debug.Log("Esco");
        }
    }
    /// <summary>
    /// Play the Sacrifice event
    /// </summary>
    public void PlaySacrificeEvent()
    {
        Instantiate(SacrificeEvent);
    }
    /// <summary>
    /// Play the escort event
    /// </summary>
    public void PlayHeartEscortEvent()
    {
        Instantiate(HeartEscortEvent);
    }
    /// <summary>
    /// Add an event to master event list
    /// </summary>
    /// <param name="newEvent"></param>
    public void AddEvent(Event newEvent)
    {
        ActiveEvents.Add(newEvent);
    }
    /// <summary>
    /// Clear all event gameobjects
    /// </summary>
    public void ClearEventObjects()
    {
        foreach (var go in activeEventGameObjects)
        {
            activeEventGameObjects.Remove(go);
            go.gameObject.SetActive(false);
        }
    }

}
