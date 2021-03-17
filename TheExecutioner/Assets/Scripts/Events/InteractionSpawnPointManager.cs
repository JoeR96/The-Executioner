using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class InteractionSpawnPointManager : MonoBehaviour
{
    [SerializeField] private GameObject shotgunPickup;
    [SerializeField] private GameObject pistolPickup;

    public void SpawnWeapon(Transform spawnPoint)
    {
        if (Random.value < 0.5f)
                Instantiate(shotgunPickup, spawnPoint.position, quaternion.identity);

            else
                Instantiate(pistolPickup, spawnPoint.position, quaternion.identity);
    }

}
