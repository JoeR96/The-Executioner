using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.position = playerSpawnPoint.position;

        if (other.CompareTag("Zombie"))
        {
            other.GetComponent<EnemyBase>().DeactivateZombie();
            other.transform.position = playerSpawnPoint.position;
        }
            
    }
}
