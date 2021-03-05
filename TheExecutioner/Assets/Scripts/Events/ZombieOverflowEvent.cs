using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOverflowEvent : MonoBehaviour
{
    [SerializeField] private Transform[] _overflowSpawns;
    [SerializeField] private GameObject[] _jailSpotsLights;
    [SerializeField] private Transform[] _jailCells;
    // Start is called before the first frame update

    public IEnumerator OverflowEvent()
    {
        InvokeRepeating("Overflow",0f,0.5f);
        yield return new WaitForSeconds(2f);
        CancelInvoke();
        yield return null;
    }

    
    public IEnumerator JailBreakEvent()
    {
        
        yield return null;
    }
    public void PlayJailBreakEvent()
    {
        var spawnArray = GameManager.instance.EnvironmentManager.SpawnPoints;
        var tempList = new List<Transform>();
        var tempHolder = new List<GameObject>();
        for (int i = 0; i < Random.Range(10,15); i++)
        {
            var random = Random.Range(0, spawnArray.Count);
            tempList.Add(spawnArray[random].transform);
            tempHolder.Add(spawnArray[random]);
            spawnArray.RemoveAt(random);
        }

        foreach (var go in tempHolder)
        {
            spawnArray.Add(go);
        }

        Transform[] spawnPosition = new Transform[tempList.Count];

        for (int i = 0; i < spawnPosition.Length; i++)
        {
            spawnPosition[i] = tempList[i];
        }
        GameManager.instance.ZombieSpawner.SpawnZombiesAtLocations(spawnPosition);
        
    }

    private void ActivateJailCells()
    {
        foreach (var cell in _jailCells)
        {
            cell.GetComponent<Animator>().SetBool("JailBreakActive", true);
        }
    }

    private void ACtivateLights()
    {
        foreach (var light  in _jailSpotsLights)
        {
            light.GetComponent<Animator>().SetBool("JailBreakActive", true);
        }
    }
    

    public void PlayOverFlowEvent()
    {
        Overflow();
        StartCoroutine(OverflowEvent());
    }

    private void Overflow()
    {
        GameManager.instance.ZombieSpawner.SpawnZombiesAtLocations(_overflowSpawns);
    }
    
    
}
