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
   [SerializeField] private RaycastWeapon[] _equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIndex;
    public RaycastWeapon CurrentRaycastWeapon;
    public Transform[] WeaponSlots;
    public Animator RigController;
    
    // Start is called before the first frame update
    void Start()
    {
        //CurrentRaycastWeapon = GetComponentInChildren<RaycastWeapon>();
        if (CurrentRaycastWeapon)
        {
            EquipWeapon(CurrentRaycastWeapon);
        }
    }

    public void SelectWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            activeWeaponIndex--;
            SetWeaponWheelIndex();
            EquipWeapon(_equippedWeapons[activeWeaponIndex]);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            activeWeaponIndex++;
            SetWeaponWheelIndex();
            EquipWeapon(_equippedWeapons[activeWeaponIndex]);
        } 
            
        
        
    }

    private void SetWeaponWheelIndex()
    {
        if (activeWeaponIndex == 3)
        {
            activeWeaponIndex = 0;
        }

        if (activeWeaponIndex == -1)
        {
            activeWeaponIndex = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SelectWeapon();
        if (CurrentRaycastWeapon)
        {
            if (Input.GetKey(KeyCode.Mouse0) && CurrentRaycastWeapon.CanFire())
            {
                CurrentRaycastWeapon.FireWeapon();
                RigController.Play("weapon_"+CurrentRaycastWeapon.WeaponName+"_fire",0);
            }
            else if(Input.GetKey(KeyCode.Mouse0) && !CurrentRaycastWeapon.CanFire())
            {
                AudioManager.Instance.PlaySound("ClipEmpty");
            }
            
            if (Input.GetKey(KeyCode.R) && !CurrentRaycastWeapon.WeaponIsReloading)
                CurrentRaycastWeapon.StartCoroutine("ReloadWeapon");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(_equippedWeapons[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(_equippedWeapons[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(_equippedWeapons[2]);
        }
    }
    
    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        CurrentRaycastWeapon = _equippedWeapons[(int) weaponSlot];
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
        yield return null;
    }

    IEnumerator ActivateWeapon(int index)
    {
        Debug.Log(index);
        SetActiveWeapon((WeaponSlot) index);
        _equippedWeapons[index].gameObject.SetActive(true);
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
        AudioManager.Instance.PlaySound("EquipWeapon");
        CurrentRaycastWeapon.gameObject.SetActive(false);
        SetActiveWeapon(weapon.weaponSlot);
        CurrentRaycastWeapon.gameObject.SetActive(true);
        RigController.Play("weapon_"+weapon.WeaponName+"_equip",0);
        activeWeaponIndex = (int) weapon.weaponSlot;    
        WeaponSlots[activeWeaponIndex].gameObject.SetActive(true);
        GetComponent<EquippedWeapon>().DisplayEquippedWeapon((int)CurrentRaycastWeapon.weaponSlot);
        CurrentRaycastWeapon.WeaponIsSet = weapon.WeaponIsSet;
        CurrentRaycastWeapon.SetWeaponState(weapon.Quality);
        





    }
}
