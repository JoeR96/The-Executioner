using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnPoints enemySpawnPoints;
    private SacrificeEvent sacrificeEvent;
    
    private void Start()
    {
        sacrificeEvent = GetComponent<SacrificeEvent>();
    }

    public void PlaySacrificeEvent()
    {
        sacrificeEvent.StartEvent(enemySpawnPoints.ReturnEventSpawnPoint());
    }

}
