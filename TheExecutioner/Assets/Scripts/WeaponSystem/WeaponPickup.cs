using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponPickup : MonoBehaviour
{
    public Color[] GodBeamColours;
    public GameObject Godbeam;

    private void Start()
    {
           var t = GetQuality();
           raycastWeapon.SetWeaponState(t);
           //SetGodBeamColour(t);
    }

    public int GetQuality()
    {
        var random = Random.Range(0, GodBeamColours.Length);
        
        return random;
    }
    

    public void SetGodBeamColour(int index)
    {
        var meshRenderer = Godbeam.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material.SetColor("_TintColor",GodBeamColours[index]);
    }
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
