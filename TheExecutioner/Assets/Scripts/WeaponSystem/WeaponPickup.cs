using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponPickup : MonoBehaviour
{
    public GameObject[] GodBeams;
    public int quality;
    private void Start()
    {
        quality = GetQuality();
        SetGodBeamColour(quality);
    }

    public int GetQuality()
    {
        var random = Random.Range(0, GodBeams.Length);
        
        return random;
    }

    public void SetGodBeamColour(int index)
    {
        GodBeams[index].gameObject.SetActive(true);
    }
    public RaycastWeapon raycastWeapon;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                var weapon = activeWeapon.CurrentRaycastWeapon;
                weapon.WeaponIsReloading = false;
                activeWeapon.EquipWeapon(raycastWeapon);
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

