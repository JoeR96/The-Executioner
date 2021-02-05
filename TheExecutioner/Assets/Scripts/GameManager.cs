using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ZombieOverflowEvent ZombieOverFlowEvent;
    public LimbSpawner LimbSpawner;
    public ZombieSpawner ZombieSpawner;
   
    void Start()
    {
        ZombieOverFlowEvent = GetComponent<ZombieOverflowEvent>();
        ZombieSpawner = GetComponent<ZombieSpawner>();
        LimbSpawner = GetComponent<LimbSpawner>();
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



