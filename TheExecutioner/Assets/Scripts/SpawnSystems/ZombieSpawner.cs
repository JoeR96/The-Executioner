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
    
    /// <summary>
    /// Spawns a basic zombie at the specified transform
    /// </summary>
    public GameObject SpawnZombie(Transform location)
    {
        var t = ObjectPooler.instance.GetObject(PoolObjectType.BasicZombie);
        t.SetActive(true);
        t.transform.SetPositionAndRotation(location.position,quaternion.identity);
        ActiveZombies.Add(t);
        return t;
    }
    /// <summary>
    /// Spawns a basic zombie at a random location 
    /// </summary>
    public GameObject SpawnZombie()
    {
        var location = enemySpawnPoints.ReturnInternalSpawnPoint();
        var t = ObjectPooler.instance.GetObject(PoolObjectType.BasicZombie);
        t.transform.SetPositionAndRotation(location.position,quaternion.identity);
        ActiveZombies.Add(t);
        t.SetActive(true);
        return t;
    }
    /// <summary>
    /// Spawns a armored zombie at a specified location
    /// </summary>
    public GameObject SpawnArmoredZombie(Transform location)
    {
        var t = ObjectPooler.instance.GetObject(PoolObjectType.FastZombie);
        t.transform.SetPositionAndRotation(location.position,quaternion.identity);
        //Needs to be added to elite list
        return t;
    }
    /// <summary>
    /// Spawns zombies in a ragdoll state at each transform in a specified list
    /// </summary>
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
    /// <summary>
    /// Spawns armored zombies at each transform in a specified list
    /// </summary>
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
    /// <summary>
    /// Spawns a zombie in a ragdoll state at a specified location
    /// </summary>
    public void SpawnRagdollZombieAtLocations(Transform location)
    {
        var t = SpawnZombie(location);
        t.GetComponent<NavMeshAgent>().enabled = false;
        t.GetComponent<Ragdoll>().ActivateRagDoll();
        t.transform.position = location.position;
        
    }
    /// <summary>
    /// Spawns an active zombie at a specified location
    /// </summary>
    public void SpawnActiveZombieAtLocation(Transform location)
    {
        var t = SpawnZombie(location);
        t.transform.position = location.position;
    }
    /// <summary>
    /// Spawns an active zombie at each location in a supplied list
    /// </summary>
    public void SpawnActiveZombiesAtLocation(List<Transform> location)
    {   
        for (int i = 0; i < location.Count; i++)
        {
            var t = SpawnZombie(location[i]);
            t.transform.position = location[i].position;
        }
        
    }
    /// <summary>
    /// Removes an active zombie from the list
    /// </summary>
    public void RemoveZombieFromList(GameObject Zombie)
    {
        ActiveZombies.Remove(Zombie);
    }
    /// <summary>
    /// Clears all zombies from a list
    /// </summary>
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
