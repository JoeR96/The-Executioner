using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool IsFiring;
    public Transform RaycastOrigin;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public TrailRenderer TracerEffect;
    public Transform RaycastDestination;
    private Ray ray;
    private RaycastHit hitInfo;
    public string WeaponName;
    public weaponRecoil recoil;

    #region weaponvariables
    [Header("Utility ")][Space(10)]
    [SerializeField] [Range(0f, 60f)]
    protected float weaponMaxAmmo;
    
    [SerializeField][Range(0f,180f)]
    protected float weaponSpareAmmo;
    
    [SerializeField] [Range(0f,5f)]
    protected float weaponReloadTime;
    
    [Header("Damage variables")][Space(10)]
    
    [SerializeField] [Range(0f,15f)]
    protected float weaponDamage;
    
    [SerializeField] [Range(0f, 2.5f)]
    protected float weaponFireRate;
    
    protected float weaponFireTimer;

    public bool WeaponIsReloading { get; protected set; }
    public bool WeaponIsLoaded { get; protected set; }
    
    protected float weaponReloadTimer;
    
    protected string weaponFiringClip;
    protected string weaponReloadingClip;
    protected string weaponName;
    
    [SerializeField]
    protected float weaponCurrentammo;

    public float WeaponCurrentammo
    {
        get => weaponCurrentammo;
        set => weaponCurrentammo = value;
    }

    public float WeaponMaxAmmo
    {
        get => weaponMaxAmmo;
        set => weaponMaxAmmo = value;
    }

    #endregion
    private void Awake()
    {
        recoil = GetComponent<weaponRecoil>();
    }
    
    protected void Start()
    {
        Reload();
        WeaponIsLoaded = true;
        weaponMaxAmmo = weaponCurrentammo;
        WeaponIsReloading = false;
    }

    protected void Update()
    {
        weaponReloadTimer += Time.deltaTime;
        weaponFireTimer += Time.deltaTime;
    }
    public void FireWeapon()
    {
        weaponFireTimer = 0f;
        weaponCurrentammo -= 1;
        recoil.Reset();
        IsFiring = true;
        MuzzleFlash.Emit(1);
        ray.origin = RaycastOrigin.position;
        ray.direction = RaycastDestination.position - RaycastOrigin.position;
        AudioManager.Instance.PlaySound("ShotgunFire");
        var tracer = Instantiate(TracerEffect, ray.origin,quaternion.identity);
        tracer.AddPosition(ray.origin);
        if(Physics.Raycast(ray,out hitInfo))
        {
            HitEffect.transform.position = hitInfo.point;
            HitEffect.transform.forward = hitInfo.normal;
            HitEffect.Emit(1);
            string name = hitInfo.collider.name;
            tracer.transform.position = hitInfo.point;
            recoil.GenerateRecoil(WeaponName);
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.GetComponentInParent<ITakeDamage>() != null)
            {
                hitInfo.collider.GetComponentInParent<ITakeDamage>().TakeDamage(100, ray.direction);
                if (hitInfo.collider.CompareTag("DestructibleLimb"))
                {
                    hitInfo.collider.GetComponentInParent<IDestroyLimb>().DestroyLimb(name, hitInfo.point);   
                }
            }
           
        }
    }

    protected virtual IEnumerator ReloadWeapon()
    {
        var test = FindObjectOfType<ActiveWeapon>();
        test.RigController.SetBool("IsReloading",true);
        WeaponIsReloading = true;
        yield return new WaitForSeconds(weaponReloadTime);
        Reload();
        
    }
    
    public bool FireRateTimer()
    {
        if (weaponFireTimer < weaponFireRate)
        {
            return false;
        }

        return true;
    }

    protected bool Reloading()
    {
        weaponReloadTimer -= Time.deltaTime;
        if (weaponReloadTimer < weaponReloadTime)
        {
            return false;
        }
        
        return true;
    }
    
    protected virtual void Reload()
    {
        var toAdd = weaponMaxAmmo - weaponCurrentammo;
        weaponCurrentammo += toAdd;
        weaponMaxAmmo -= toAdd;
        WeaponIsReloading = false;
        
        
    }

    public bool CanFire()
    {
        if (!WeaponIsReloading && FireRateTimer() && weaponCurrentammo != 0)
            return true;

        return false;
    }
}
