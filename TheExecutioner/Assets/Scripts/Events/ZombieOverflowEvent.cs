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
        GameManager.instance.ZombieSpawner.SpawnZombiesAtLocations(_jailCells);
        foreach (var cell in _jailCells)
        {
            cell.GetComponent<Animator>().SetBool("JailBreakActive", true);
            
        }

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
