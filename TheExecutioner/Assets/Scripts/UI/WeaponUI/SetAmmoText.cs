using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetAmmoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private ActiveWeapon activeWeapon;

    private void Update()
    {
        Debug.Log(activeWeapon.CurrentRaycastWeapon.WeaponName);
        ammoText.SetText(activeWeapon.CurrentRaycastWeapon.WeaponCurrentammo 
                         + "  |  " + 
                         activeWeapon.CurrentRaycastWeapon.WeaponMaxAmmo);
    }
}
