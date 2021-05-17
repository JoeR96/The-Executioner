using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbManager : MonoBehaviour
{
    
    [SerializeField] protected ParticleSystem[] _deathParticles;
    [field:SerializeField] public GameObject[] Limbs { get; set; }

    public Dictionary<string, Transform> DestructibleLimbs { get; } = new Dictionary<string, Transform>();
    private Dictionary<String, ParticleSystem> _destructibleLimbParticle = new Dictionary<string, ParticleSystem>();

    public LimbParticleLocation[] _LimbParticleLocations;
    public ParticleSystem _bloodSplat;


    private void Awake()
    {
        PopulateLimbDictionary();
    }

    private void PopulateLimbDictionary()
    {
        foreach (var gameobject in Limbs)
        {
            DestructibleLimbs.Add(gameobject.transform.name, gameobject.transform);
        }
    }

    public void PlayParticleAtLimb(string limbName)
    {
        var limb = Array.Find(_LimbParticleLocations,
            zombieLimb => zombieLimb.Name == limbName);
        var t = limb.Location;

        _bloodSplat.transform.SetParent(t);
        _bloodSplat.transform.SetPositionAndRotation(t.position, t.rotation);
        _bloodSplat.Play();

    }
   
    public IEnumerator RemoveLimb(GameObject toDestroy,float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(toDestroy);
    }
    public void PlayDeathParticles()
    {
        foreach (var particle in _deathParticles)
        {
            particle.Play();
        }
        
    }
}

[Serializable]
public struct LimbParticleLocation
{
    public string Name;
    public Transform Location;
}