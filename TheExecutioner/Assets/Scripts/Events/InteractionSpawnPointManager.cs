using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class InteractionSpawnPointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
   

    public void SpawnWeapon(Transform spawnPoint)
    {
        var rand = Random.Range(0, weapons.Count);
        var randomWeapon = weapons[rand];
                Instantiate(randomWeapon, spawnPoint.position, quaternion.identity);

    }

}
