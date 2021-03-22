using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public ZombieSpawner ZombieSpawner;

    private void Start()
    {
        ZombieSpawner = GetComponent<ZombieSpawner>();
    }
}
