using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private Transform navmeshPos;
    public List<GameObject> ActiveFodderZombies = new List<GameObject>();
    public GameObject SpawnZombie(Transform location)
    {
        var t = Instantiate(_zombiePrefab,location.position, Quaternion.identity);
        t.transform.position = location.position;
        return t;
    }
    
    public void SpawnRagdollZombiesAtLocations(List<Transform> location)
    {
        for (int i = 0; i < location.Count; i++)
        {
            ActiveFodderZombies.Add(gameObject);
            var t = SpawnZombie(location[i]);
            t.GetComponent<NavMeshAgent>().enabled = false;
             t.GetComponent<Ragdoll>().ActivateRagDoll();
             t.transform.position = location[i].position;
        }
    }
    public void SpawnRagdollZombieAtLocations(Transform location)
    {
        var t = SpawnZombie(location);
        t.GetComponent<NavMeshAgent>().enabled = false;
        t.GetComponent<Ragdoll>().ActivateRagDoll();
        t.transform.position = location.position;
        
    }
    public void SpawnActiveZombieAtLocation(Transform location)
    {
        var t = SpawnZombie(location);
        t.transform.position = location.position;
    }
    
    public void SpawnActiveZombiesAtLocation(List<Transform> location)
    {   
        for (int i = 0; i < location.Count; i++)
        {
            var t = SpawnZombie(location[i]);
            t.transform.position = location[i].position;
        }
        
    }
    
}
