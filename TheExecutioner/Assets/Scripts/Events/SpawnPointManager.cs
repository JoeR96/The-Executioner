using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private GameObject shotgunPickup;
    [SerializeField] private GameObject pistolPickup;

    public void SpawnWeapon(GameObject spawnPoint)
    {

            if (Random.value < 0.5f)
                Instantiate(shotgunPickup, spawnPoint.transform.position, quaternion.identity);

            else
                Instantiate(pistolPickup, spawnPoint.transform.position, quaternion.identity);

        
    }

}
