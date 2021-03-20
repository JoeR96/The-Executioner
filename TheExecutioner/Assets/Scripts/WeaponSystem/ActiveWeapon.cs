using System.Collections;
using UnityEngine;



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
            
            if (Input.GetKey(KeyCode.Mouse0))
            {
                weapon.StartFiring();
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
        weapon.recoil.CharacterAiming = CharacterAiming;
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
