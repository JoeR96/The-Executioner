using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;

    public void SpawnZombie(Transform location)
    {
        Instantiate(_zombiePrefab, location.position, Quaternion.identity);
    }
}
