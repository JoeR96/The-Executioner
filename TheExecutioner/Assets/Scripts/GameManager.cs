using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public ZombieOverflowEvent ZombieOverFlowEvent;
    public LimbSpawner LimbSpawner;
    public ZombieSpawner ZombieSpawner;
    public EnvironmentManager EnvironmentManager;
    public Pathfinding pathfinding;
    public Grid Grid;
    
    void Awake()
    {
        
        EnvironmentManager = GetComponentInChildren<EnvironmentManager>();
        ZombieOverFlowEvent = GetComponentInChildren<ZombieOverflowEvent>();
        ZombieSpawner = GetComponentInChildren<ZombieSpawner>();
        LimbSpawner = GetComponentInChildren<LimbSpawner>();
        pathfinding = GetComponentInChildren<Pathfinding>();
        Grid = GetComponentInChildren<Grid>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            ZombieOverFlowEvent.PlayOverFlowEvent();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            ZombieOverFlowEvent.PlayJailBreakEvent();
        }
    }




    
}



