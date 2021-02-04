using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LimbSpawner LimbSpawner;
    public ZombieSpawner ZombieSpawner;
   
    void Start()
    {
        ZombieSpawner = GetComponent<ZombieSpawner>();
        LimbSpawner = GetComponent<LimbSpawner>();
    }


    
}
