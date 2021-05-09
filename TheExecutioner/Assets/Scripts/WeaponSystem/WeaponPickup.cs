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
    private RaycastWeapon raycastWeapon;
    public RaycastWeapon weapon { get; set; }

    /// <summary>
    /// Reference the weapon to pickup
    /// </summary>
    private void Awake()
    {
        weapon = GetComponentInChildren<RaycastWeapon>();
    }
    /// <summary>
    /// Set the weapon quality
    /// </summary>
    private void Start()
    {
        quality = GetQuality();
        SetGodBeamColour(quality);
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
            Debug.Log(activeWeapon);
            if (activeWeapon)
            {
                
                weapon.WeaponIsReloading = false;
                
                activeWeapon.EquipWeapon(weapon);
                weapon.ResetWeaponState();
                weapon.SetWeaponState(quality);
                
                if(!weapon.WeaponIsLoaded)
                    weapon.Reload();
                
                if(weapon.GetComponent<RPG>())
                    weapon.Reload();
            }
            Destroy(gameObject);
        }
       
            
    }

    
}

