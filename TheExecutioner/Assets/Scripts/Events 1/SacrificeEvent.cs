using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject altarPrefab;
    private Transform sacrificeTargetPosition;
    private int currentKillCount;
    private int targetKillCount;
    private int limbKillCount;
    private int limbBonusTargetCount;

    public void StartEvent(Transform eventSpawnPoint)
    {
        Instantiate(altarPrefab, eventSpawnPoint.position,Quaternion.identity);

    }
    
}
