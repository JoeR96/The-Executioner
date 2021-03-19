using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon)
            {
                RaycastWeapon newWeapon = Instantiate(weaponPrefab);
                activeWeapon.EquipWeapon(newWeapon);
            }
            Destroy(gameObject);
        }
       
            
    }
}
