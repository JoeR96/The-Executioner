using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class RaycastWeapon : MonoBehaviour
{

    
    #region weaponvariables
    [Header("Utility ")][Space(10)]
    [SerializeField] [Range(0f, 60f)]
    protected float  weaponMaxAmmo;
    
    [SerializeField][Range(0f,180f)]
    protected float weaponSpareAmmo;
    
    [SerializeField] [Range(0f,5f)]
    protected float weaponReloadTime;
    
    [Header("Damage variables")][Space(10)]
    
    [SerializeField] [Range(0f,150f)]
    protected float weaponDamage;
    
    [SerializeField] [Range(0f, 2.5f)]
    protected float weaponFireRate;
    
    protected float weaponFireTimer;

   
    [field: SerializeField] public bool WeaponIsReloading { get; set; }
    public bool WeaponIsLoaded { get; protected set; }
    
    protected float weaponReloadTimer;

    private float[] defaultValues = new float[6];
    
    protected string weaponFiringClip;
    protected string weaponReloadingClip;
    protected string weaponName;
    
    [SerializeField]
    protected float weaponCurrentammo;
    
    public ActiveWeapon.WeaponSlot weaponSlot;
    public bool IsFiring;
    public Transform RaycastOrigin;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public TrailRenderer TracerEffect;
    public Transform RaycastDestination;
    [field:SerializeField] public int Quality { get; set; }
    protected Ray ray;
    private RaycastHit hitInfo;
    public string WeaponName;
    public weaponRecoil recoil;
    protected string reloadWeapon;
    protected string fireWeapon;
    protected string equipWeapon;
    #endregion

    #region properties
    
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
    
    public float WeaponDamage
    {
        get => weaponDamage;
        set => weaponDamage = value;
    }
    public float WeaponReloadTimer
    {
        get => weaponReloadTimer;
        set => weaponReloadTimer = value;
    }
    
    public float WeaponReloadTime
    {
        get => weaponReloadTime;
        set => weaponReloadTime = value;
    }
    
    public float WeaponSpareAmmo
    {
        get => weaponSpareAmmo;
        set => weaponSpareAmmo = value;
    }

    public bool WeaponIsSet { get; set; }

    #endregion
    private void Awake()
    {
        recoil = GetComponent<weaponRecoil>();
    }
    
    protected void Start()
    {
        WeaponIsSet = false;
        Reload();
        
        WeaponIsLoaded = true;
    
        
        weaponCurrentammo = weaponMaxAmmo;
        WeaponIsReloading = false;
        weaponSpareAmmo = weaponMaxAmmo * 2;
        defaultValues[1] = weaponDamage;
        defaultValues[2] = weaponMaxAmmo;
        defaultValues[3] = weaponSpareAmmo;
        defaultValues[4] = weaponReloadTime;
        defaultValues[5] = weaponReloadTimer;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
            IsFiring = false;
        weaponReloadTimer += Time.deltaTime;
        weaponFireTimer += Time.deltaTime;
    }

    protected void OnEnable()
    {
        WeaponIsReloading = false;
    }
    #region weaponlogic
    
    public virtual void FireWeapon()
    {
        SetWeaponProperties();
        SetRaycastPositions();
        AudioManager.Instance.PlaySound(fireWeapon);
        MuzzleFlash.Emit(1);
        var tracer = InstantiateTrailRenderer();
        if(Physics.Raycast(ray,out hitInfo))
        {
            
            SetHitEffects();
            tracer.transform.position = hitInfo.point;
            recoil.GenerateRecoil(WeaponName);
            if (hitInfo.collider.GetComponentInParent<ITakeDamage>() != null)
            {
                HitEnemy();
            }
            else
            {
                
            }
        }
    }

    protected TrailRenderer InstantiateTrailRenderer()
    {
        var tracer = Instantiate(TracerEffect, ray.origin, quaternion.identity);
        tracer.AddPosition(ray.origin);
        return tracer;
    }

    protected void HitEnemy()
    {
        hitInfo.collider.GetComponentInParent<ITakeDamage>().TakeDamage(weaponDamage, ray.direction);
        if (hitInfo.collider.CompareTag("DestructibleLimb"))
        {
            hitInfo.collider.GetComponentInParent<IDestroyLimb>().DestroyLimb(hitInfo.collider.name, hitInfo.point);
        }
    }

    protected void SetHitEffects()
    {
        HitEffect.transform.position = hitInfo.point;
        HitEffect.transform.forward = hitInfo.normal;
        HitEffect.Emit(1);
    }

    protected virtual void SetWeaponProperties()
    {
        weaponFireTimer = 0f;
        weaponCurrentammo--;
        recoil.Reset();
        IsFiring = true;
    }

    protected void SetRaycastPositions()
    {
        ray.origin = RaycastOrigin.position;
        ray.direction = RaycastDestination.position - RaycastOrigin.position;
    }

    protected virtual IEnumerator ReloadWeapon()
    {
        var test = FindObjectOfType<ActiveWeapon>();
        test.RigController.Play("weapon_reload_" + test.CurrentRaycastWeapon.WeaponName,0);
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

    public virtual void Reload()
    {
        WeaponIsReloading = false;
        
        if(weaponSpareAmmo <= 0)
            return;
    
        AudioManager.Instance.PlaySound(reloadWeapon);
        var toAdd = weaponMaxAmmo - weaponCurrentammo;
        for (int i = 0; i < toAdd; i++)
        {
            if(weaponSpareAmmo == 0)
                return;
            weaponCurrentammo++;
            weaponSpareAmmo--;
        }
    }

    public bool CanFire()
    {
        if (!WeaponIsReloading && FireRateTimer() && weaponCurrentammo != 0)
            return true;

        return false;
    }
    #endregion

    public float ReturnDefaultValue(int index)
    {
        return defaultValues[index];
    }
    //refactor this in to non mono class
    //multiply and divide by an input to recieve an output
    public void SetWeaponState(int quality)
    {
        if (WeaponIsSet == false)
        {
            ResetWeaponState();
            Quality = quality;
            GameManager.instance.PrintGameUi.SetWeaponColour(weaponSlot,quality);
            weaponDamage *= quality + 1;
            if (WeaponName != "RPG")
            {
                weaponCurrentammo *= quality + 1; 
             
            }
            weaponMaxAmmo *= quality + 1; 
            weaponSpareAmmo *= quality + 1;
            weaponReloadTime /= quality + 1; 
            weaponReloadTimer /= quality + 1; 
            Mathf.RoundToInt(weaponDamage);
            Mathf.RoundToInt(weaponMaxAmmo);
            Mathf.RoundToInt(weaponSpareAmmo);
            WeaponIsSet = true;
        }
    }

    public void ResetWeaponState()
    {
        weaponCurrentammo = ReturnDefaultValue(2);
        weaponDamage = ReturnDefaultValue(1);
        weaponMaxAmmo = ReturnDefaultValue(2);
        weaponSpareAmmo = ReturnDefaultValue(3);
        weaponReloadTime = ReturnDefaultValue(4);
        weaponReloadTimer = ReturnDefaultValue(5);
    }


}
