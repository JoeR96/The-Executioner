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
    public RaycastWeapon raycastWeapon;
    private void Start()
    {
        quality = GetQuality();
        SetGodBeamColour(quality);
    }

    public int GetQuality()
    {
        var random = Random.Range(0, GodBeams.Length -1);
        
        return random;
    }

    public void SetGodBeamColour(int index)
    {
        Debug.Log(index);
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
                weapon = activeWeapon.CurrentRaycastWeapon;
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

    public RaycastWeapon weapon { get; set; }
}

