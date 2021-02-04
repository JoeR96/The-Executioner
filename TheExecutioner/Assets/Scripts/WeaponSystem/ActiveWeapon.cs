using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        PrimaryWeapon = 0,
        SecondaryWeapon = 1,
        RocketLauncher = 2,
        Grenade = 3
    }

    public CinemachineFreeLook PlayerCamera;
    public Transform CrossHairTarget;
    private RaycastWeapon[] _equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    
    public Transform[] WeaponSlots;

    public Animator RigController;

 
    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
        }
    }

    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= _equippedWeapons.Length)
            return null;
        return _equippedWeapons[index];
        
    }
    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon)
        {
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weapon.StartFiring();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                weapon.StopFiring();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                ToggleWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.PrimaryWeapon);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.SecondaryWeapon);
        }


    }

    private void ToggleWeapon()
    {
        bool isHolstered = RigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }
    public void EquipWeapon(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int) newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.RaycastDestination = CrossHairTarget;
        weapon.recoil.Playercamera = PlayerCamera;
        weapon.recoil.RigController = RigController;
        weapon.transform.SetParent(WeaponSlots[weaponSlotIndex],false);
        _equippedWeapons[weaponSlotIndex] = weapon;
        
        SetActiveWeapon(newWeapon.weaponSlot);
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;
        if (holsterIndex == activateIndex)
            holsterIndex = -1;
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }
    IEnumerator HolsterWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            RigController.SetBool("holster_weapon",true);
            do
            {
                yield return new WaitForEndOfFrame();
            }while (RigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            RigController.SetBool("holster_weapon", false);
            RigController.Play("equip_" + weapon.WeaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (RigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activeIndex));
        activeWeaponIndex = activeIndex;
    }
}
