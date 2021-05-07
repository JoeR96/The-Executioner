using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSpawner : MonoBehaviour
{
    [SerializeField] private ZombieLimb[] _zombieLimbs;

    /// <summary>
    /// Using a passed string spawn a limb from a struct
    /// Use Array.Find to match the string to the limb name within the struct
    /// Instantiate the limb
    /// Return the object
    /// </summary>
    /// <param name="limbName"></param>
    /// <returns></returns>
    public GameObject ReturnLimb(string limbName)
    {
        var limb = Array.Find(_zombieLimbs, 
            zombieLimb => zombieLimb.LimbName == limbName);
        var t = Instantiate(limb.LimbPrefab);
        return t;
    }
    /// <summary>
    /// I wanted to serialize a dictionary for this structure,
    /// In hindsight I could have used a scriptable object however a serialized struct served an appropriate purpose
    /// The Limb gameobject can be considered the value whilst the LimbName string can be considered the key.
    /// </summary>
    [Serializable]
    public struct ZombieLimb
    {
        public GameObject LimbPrefab;
        public String LimbName;
    }
}
