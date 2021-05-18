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
    [SerializeField] public RaycastWeapon[] _equippedWeapons = new RaycastWeapon[2];
    public CharacterAiming CharacterAiming;
    public Transform CrossHairTarget;
    private int activeWeaponIndex;
    public RaycastWeapon CurrentRaycastWeapon;
    public Transform[] WeaponSlots;
    public Animator RigController;
    /// <summary>
    /// Initialize weapons
    /// </summary>
    void Start()
    {
        for (int i = 2 ; i >= 0 ; i--)
        {
            EquipWeapon(_equippedWeapons[i]);
        }
    }
    /// <summary>
    /// Use scroll wheel up or down
    /// to increase or decrease an index
    /// </summary>
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
    /// <summary>
    /// set the weapon accordingly
    /// reset the index if required
    /// </summary>
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
    /// <summary>
    /// Activate weapon with an input parameter
    /// </summary>
    /// <param name="weaponSlot"></param>
    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        CurrentRaycastWeapon = _equippedWeapons[(int) weaponSlot];
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
