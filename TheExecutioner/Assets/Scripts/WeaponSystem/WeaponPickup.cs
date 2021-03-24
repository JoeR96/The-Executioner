using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon raycastWeapon;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                activeWeapon.CurrentRaycastWeapon.SetWeaponState(0.75f);
                activeWeapon.EquipWeapon(raycastWeapon);
            }
            Destroy(gameObject);
        }
       
            
    }
}
