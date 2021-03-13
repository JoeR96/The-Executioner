using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSpawner : MonoBehaviour
{
    [SerializeField] private ZombieLimb[] _zombieLimbs;

    public GameObject ReturnLimb(string limbName)
    {
        var limb = Array.Find(_zombieLimbs, 
            zombieLimb => zombieLimb.LimbName == limbName);
        var t = Instantiate(limb.LimbPrefab);
        return t;
    }
    
    [Serializable]
    public struct ZombieLimb
    {
        public GameObject LimbPrefab;
        public String LimbName;
    }
}
