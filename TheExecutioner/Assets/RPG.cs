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
        TracerEffect = null;
    }

    public override void FireWeapon()
    {
        if (!WeaponIsReloading && WeaponIsLoaded)
        {
            AudioManager.Instance.PlaySound("RPGFire");
            SetRaycastPositions();
            SetWeaponProperties();
            
            MuzzleFlash.Play();
        }
    }

    protected override void Reload()
    {
        base.Reload();
        activeRocket = Instantiate(rocketPrefab, rocketPosition);
    }

    protected override void SetWeaponProperties()
    {
        weaponCurrentammo--;
        weaponFireTimer = 0f;
        StartCoroutine(LerpRocket(ray.direction));
    }

    protected override IEnumerator ReloadWeapon()
    {
        WeaponIsReloading = true;
        AudioManager.Instance.PlaySound(weaponReloadingClip);
        yield return new WaitForSeconds(2f);
        Reload();
        WeaponIsLoaded = true;
    }
    
    private IEnumerator LerpRocket(Vector3 target)
    {
        float timer = 0;

        Vector3 start = activeRocket.transform.position; 
        
        while (timer < rocketForce)
        {
            Debug.Log("HI");
            timer += Time.deltaTime;
            float percentage = Mathf.Min(timer / rocketForce, 1);
            activeRocket.transform.position = Vector3.Lerp(start, target, 1);
            yield return null;
        }
        Destroy(activeRocket);
        Reload();
    }
}