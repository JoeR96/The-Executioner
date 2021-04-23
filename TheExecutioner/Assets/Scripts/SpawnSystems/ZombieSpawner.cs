using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnPoints enemySpawnPoints;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private GameObject _zombieArmorPrefab;
    [SerializeField] private Transform navmeshPos;
    public List<GameObject> ActiveZombies = new List<GameObject>();
    public List<GameObject> EliteZombie = new List<GameObject>();

    private void Update()
    {
        if(Input.GetKey(KeyCode.P))
            ClearZombies();
    }
    public GameObject SpawnZombie(Transform location)
    {
        var t = ObjectPooler.instance.GetObject(PoolObjectType.BasicZombie);
        SetZombiePosition(t,location);
        ActiveZombies.Add(t);
        return t;
    }

    public GameObject SpawnZombie()
    {
        var location = enemySpawnPoints.ReturnInternalSpawnPoint();
        var t = ObjectPooler.instance.GetObject(PoolObjectType.BasicZombie);
        ActiveZombies.Add(t);
        SetZombiePosition(t, location);
        return t;
    }

    private void SetZombiePosition(GameObject t, Transform location)
    {
        t.transform.SetPositionAndRotation(location.position, quaternion.identity);
        t.transform.position = location.position;
    }

    public GameObject SpawnArmoredZombie(Transform location)
    {
        var t = ObjectPooler.instance.GetObject(PoolObjectType.FastZombie);
        t.transform.SetPositionAndRotation(location.position,quaternion.identity);
        t.transform.position = location.position;
        return t;
    }
    
    public void SpawnRagdollZombiesAtLocations(List<Transform> location)
    {
        
        for (int i = 0; i < location.Count; i++)
        {
            if(ActiveZombies.Count >= 30)
                return;
            var t = SpawnZombie(location[i]);
            t.GetComponent<NavMeshAgent>().enabled = false;
             t.GetComponent<Ragdoll>().ActivateRagDoll();
             t.transform.position = location[i].position;
             ActiveZombies.Add(t);
        }
    }
    public void SpawnArmoredZombiesAtLocations(List<Transform> location)
    {
        for (int i = 0; i < location.Count; i++)
        {
            
            var t = SpawnArmoredZombie(location[i]);
            t.GetComponent<NavMeshAgent>().enabled = false;
            t.GetComponent<Ragdoll>().ActivateRagDoll();
            t.transform.position = location[i].position;
            EliteZombie.Add(t);
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

    public void RemoveZombieFromList(GameObject Zombie)
    {
        ActiveZombies.Remove(Zombie);
    }

    public void ClearZombies()
    {
 
            for (int i = 0; i < ActiveZombies.Count; i++)
            {
                ActiveZombies.RemoveAt(i);
            }
            
            foreach (var zombie in EliteZombie)
        {
            EliteZombie.Remove(zombie);
        }
    }
}
