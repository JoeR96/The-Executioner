using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        PrimaryWeapon = 0,
        SecondaryWeapon = 1,
        SuperWeapon = 2,
        Grenade = 3
    }

    public CharacterAiming CharacterAiming;
    public Transform CrossHairTarget;
    private RaycastWeapon[] _equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    public RaycastWeapon CurrentRaycastWeapon;
    public Transform[] WeaponSlots;

    public Animator RigController;

 
    // Start is called before the first frame update
    void Start()
    {
        CurrentRaycastWeapon = GetComponentInChildren<RaycastWeapon>();
        if (CurrentRaycastWeapon)
        {
            EquipWeapon(CurrentRaycastWeapon);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (CurrentRaycastWeapon)
        {
            if (Input.GetKey(KeyCode.Mouse0) && CurrentRaycastWeapon.CanFire())
            {
                CurrentRaycastWeapon.FireWeapon();
                RigController.Play("weapon_"+CurrentRaycastWeapon.WeaponName+"_fire",0);
            }
            
            if (Input.GetKey(KeyCode.R) && !CurrentRaycastWeapon.WeaponIsReloading)
                CurrentRaycastWeapon.StartCoroutine("ReloadWeapon");
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
    
    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;
        if (holsterIndex == activateIndex)
            holsterIndex = -1;
        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }
    
    IEnumerator SwitchWeapon(int holsterIndex, int activeIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activeIndex));
        activeWeaponIndex = activeIndex;
    }
    IEnumerator HolsterWeapon(int index)
    {
        var weapon = GetWeapon(index);
        // if (weapon)
        // {
        //     RigController.SetBool("holster_weapon",true);
        //     do
        //     {
        //         yield return new WaitForEndOfFrame();
        //     }while (RigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        // }
        yield return null;
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            
            RigController.Play("equip_" + weapon.WeaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (RigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        yield break;
    }
    
    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= _equippedWeapons.Length)
            return null;
        return _equippedWeapons[index];
        
    }
    public void EquipWeapon(RaycastWeapon weapon)
    {
        RigController.Play("weapon_"+weapon.WeaponName+"_equip",0);
        activeWeaponIndex = (int) weapon.weaponSlot;    
        WeaponSlots[activeWeaponIndex].gameObject.SetActive(true);
    }
}
