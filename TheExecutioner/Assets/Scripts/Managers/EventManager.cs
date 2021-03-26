using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;
    private SacrificeEvent sacrificeEvent;
    private List<Transform> activeEvents = new List<Transform>();
    private void Start()
    {
        sacrificeEvent = GetComponent<SacrificeEvent>();
    }

    public void PlaySacrificeEvent()
    {
        var eventLocation = enemySpawnPoints.ReturnEventSpawnPoint();
        sacrificeEvent.StartEvent(eventLocation);
        activeEvents.Add(eventLocation);
    }

    public Transform ReturnRandomEventLocation()
    {
        var random = Random.Range(0, activeEvents.Count);
        return activeEvents[random];
    }

}
