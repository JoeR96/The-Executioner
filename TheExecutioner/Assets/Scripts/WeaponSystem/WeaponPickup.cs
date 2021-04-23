using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponPickup : MonoBehaviour
{
    public GameObject[] GodBeams;
    private int quality;
    private void Start()
    {
        
        quality = GetQuality();
           raycastWeapon.SetWeaponState(quality);
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
                activeWeapon.CurrentRaycastWeapon.WeaponIsReloading = false;
                activeWeapon.CurrentRaycastWeapon.SetWeaponState(quality);
                activeWeapon.EquipWeapon(raycastWeapon);
            }
            Destroy(gameObject);
        }
       
            
    }
}

