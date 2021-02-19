using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform navmeshPos;
    public GameObject SpawnZombie(Transform location)
    {
        
        var t = Instantiate(_zombiePrefab, navmeshPos.position, Quaternion.identity);
        t.transform.position = location.position;
        return t;
    }
    
    public void SpawnZombiesAtLocations(Transform[] location)
    {
        
        for (int i = 0; i < location.Length; i++)
        {
            var t = SpawnZombie(navmeshPos);
             
             t.GetComponent<NavMeshAgent>().enabled = false;
             t.GetComponent<Ragdoll>().ActivateRagDoll();
             t.transform.position = location[i].position;
        }
        
    }
}
