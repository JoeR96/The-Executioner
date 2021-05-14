using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject[] GodBeams;
    private int quality;
    public bool EventReward { get; set; }
    public RaycastWeapon Weapon { get; set; }

    /// <summary>
    /// Reference the weapon to pickup
    /// </summary>
    private void Awake()
    {
        Weapon = GetComponentInChildren<RaycastWeapon>();
    }
    /// <summary>
    /// Set the weapon quality randomly if this object wasnt spawned by an event
    /// </summary>
    private void Start()
    {
        if (!EventReward)
        {
            quality = GetQuality();
            SetGodBeamColour(quality);
            
        }
        Weapon.Quality = quality;
        Weapon.WeaponIsSet = false;

    }
    private void Update()
    {
        Weapon.gameObject.transform.Rotate( new Vector3(0, 1f,0) );
    }
    /// <summary>
    /// Randomly choose a colour quality between green, blue and purple
    /// Legendary beam is ignored
    /// </summary>
    /// <returns></returns>
    public int GetQuality()
    {
        var random = Random.Range(0, GodBeams.Length -1);
        
        return random;
    }
    /// <summary>
    /// Set the God Beam quality to the input index
    /// Set the Weapon quality to the input index
    /// Set the quality to the input index
    /// </summary>
    /// <param name="index"></param>
    public void SetGodBeamColour(int index)
    {
        GodBeams[index].gameObject.SetActive(true);
        quality = index;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                Weapon.WeaponIsReloading = false;
                Weapon.Quality = quality;
                activeWeapon.EquipWeapon(Weapon);
                
                if(!Weapon.WeaponIsLoaded)
                    Weapon.Reload();
                
                if(Weapon.GetComponent<RPG>())
                    Weapon.Reload();
            }
            Destroy(gameObject);
        }
       
            
    }

    
}

