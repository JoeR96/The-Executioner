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
            Debug.Log("TESTICLES");
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
    }
    
    public void EquipWeapon(RaycastWeapon weapon)
    {
        RigController.Play("weapon_"+weapon.WeaponName+"_equip",0);
        activeWeaponIndex = (int) weapon.weaponSlot;    
    }
}
