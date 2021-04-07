using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : RaycastWeapon
{
    private GameObject toFire;

    public RPG()
    {
        weaponFireRate = 2f;
        weaponReloadTime = 2f;
        weaponFiringClip = "RpgLaunch";
        weaponReloadingClip = "RpgReload";
       
    }

    public override void FireWeapon()
    {
        if (!WeaponIsReloading && WeaponIsLoaded)
        {
            SetRaycastPositions();
            AudioManager.Instance.PlaySound("RPGFire");
            MuzzleFlash.Play();
            weaponCurrentammo --;
            WeaponIsLoaded = false;
        }
    }

    protected override void SetWeaponProperties()
    {
        weaponCurrentammo--;
        weaponFireTimer = 0f;
    }

    protected override IEnumerator ReloadWeapon()
    {
        WeaponIsReloading = true;
        AudioManager.Instance.PlaySound(weaponReloadingClip);
        yield return new WaitForSeconds(2f);
        //get current
        //add maxammo - current ammo
        //spare ammo - maxammo - current ammo 
        Reload();
        WeaponIsLoaded = true;
    }

    protected override void Reload()
    {
        toFire = Instantiate(bullet, bulletSpawn.position, transform.rotation);
        toFire.transform.SetParent(bulletSpawn);
        WeaponIsReloading = false;
    }

}