using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class RaycastWeapon : MonoBehaviour
{
    public ActiveWeapon.WeaponSlot WeaponSlot;
    public bool IsFiring;
    public Transform RaycastOrigin;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public Transform RaycastDestination;
    [field:SerializeField] public int Quality { get; set; }
    protected Ray ray;
    private RaycastHit hitInfo;
    public string WeaponName;
    public weaponRecoil recoil;
    protected string reloadWeapon;
    protected string fireWeapon;
    protected string equipWeapon;
    
    
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
  
    [SerializeField]
    protected float weaponCurrentammo;
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
        if(Physics.Raycast(ray,out hitInfo))
        {
            Debug.Log(hitInfo.collider.name);
            SetHitEffects();
            recoil.GenerateRecoil(WeaponName);
            if (hitInfo.collider.GetComponentInParent<ITakeDamage>() != null)
            {
                HitEnemy();
            }
        }
    }
    /// <summary>
    /// Access Take damage interface
    /// Deal damage and direction of impact
    /// if a destructible limb is hit
    /// Destroy the limb through the limb interface
    /// </summary>
    protected void HitEnemy()
    {
        hitInfo.collider.GetComponentInParent<ITakeDamage>().TakeDamage(weaponDamage, ray.direction);
        if (hitInfo.collider.CompareTag("DestructibleLimb"))
        {
            hitInfo.collider.GetComponentInParent<IDestroyLimb>().DestroyLimb(hitInfo.collider.name, hitInfo.point);
        }
    }
    /// <summary>
    /// Set impact particle hit position
    /// Play one particle
    /// </summary>
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
    /// <summary>
    /// Return if there is no ammo left
    /// Play the reload sound
    /// Add the required amount of ammo
    /// remove the added ammo from the spare ammo
    /// </summary>
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
    /// <summary>
    /// Return a boolean if the waepon is not 
    /// </summary>
    /// <returns></returns>
    public bool CanFire()
    {
        if (!WeaponIsReloading && FireRateTimer() && weaponCurrentammo != 0)
            return true;

        return false;
    }
    #endregion

    #region WeaponState
    /// <summary>
    /// Return a value of the weapon stats array through an index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float ReturnDefaultValue(int index)
    {
        return defaultValues[index];
    }
    /// <summary>
    /// If the weapon has not had its stats set
    /// Set each  state using the quality modifier
    /// the + 1 is added for scaling reasons aswell as to ignore multiplying by 0
    /// </summary>
    /// <param name="quality"></param>
    public void SetWeaponState(int quality)
    {
        if (WeaponIsSet == false)
        {
            ResetWeaponState();
            Quality = quality;
            GameManager.instance.PrintGameUi.SetWeaponColour(WeaponSlot,quality);
            weaponDamage *= quality + 1;    
            weaponMaxAmmo *= quality + 1; 
            weaponSpareAmmo *= quality + 1; 
            weaponCurrentammo *= quality + 1; 
            weaponReloadTime /= quality + 1; 
            weaponReloadTimer /= quality + 1; 
   
            WeaponIsSet = true;
        }
    }
    /// <summary>
    /// Reset each stat using the default value index
    /// </summary>
    public void ResetWeaponState()
    {
        weaponCurrentammo = ReturnDefaultValue(2);
        weaponDamage = ReturnDefaultValue(1);
        weaponMaxAmmo = ReturnDefaultValue(2);
        weaponSpareAmmo = ReturnDefaultValue(3);
        weaponReloadTime = ReturnDefaultValue(4);
        weaponReloadTimer = ReturnDefaultValue(5);
    }
    #endregion



}
