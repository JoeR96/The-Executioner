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
            Debug.Log("Tits");
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                activeWeapon.CurrentRaycastWeapon.SetWeaponState(3);
                activeWeapon.EquipWeapon(raycastWeapon);
            }
            Destroy(gameObject);
        }
       
            
    }
}
