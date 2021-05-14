using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RPG : RaycastWeapon
{
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject activeRocket;
    [SerializeField] private Transform rocketPosition;
    [SerializeField] private float rocketForce;
    public RPG()
    {
        fireWeapon = "RpgFire";
        reloadWeapon = "RpgReload";
        WeaponIsLoaded = false;
    }

    public override void FireWeapon()
    {
        if (!WeaponIsReloading && WeaponIsLoaded)
        {
            AudioManager.Instance.PlaySound("RPGFire");
            SetRaycastPositions();
            SetWeaponProperties();
            SetRocketRigidBodyVariables();
            activeRocket.transform.SetParent(null);
            activeRocket.GetComponent<Rocket>().SetActiveRocket();
            MuzzleFlash.Play();
        }
    }

    private void SetRocketRigidBodyVariables()
    {
        var rb = activeRocket.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = rocketPosition.forward * rocketForce;
    }

    public override void Reload()
    {
        if(weaponSpareAmmo <= 0)
            return;
        
        AudioManager.Instance.PlaySound(reloadWeapon);
        base.Reload();
        WeaponIsReloading = false;
        activeRocket = Instantiate(rocketPrefab, rocketPosition);
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
        Reload();
        WeaponIsLoaded = true;
    }


}