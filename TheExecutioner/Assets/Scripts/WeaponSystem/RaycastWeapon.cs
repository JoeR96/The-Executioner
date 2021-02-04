using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
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

    private void Start()
    {
        recoil = GetComponent<weaponRecoil>();
    }
    public void StartFiring()
    {
        recoil.Reset();
        IsFiring = true;
        MuzzleFlash.Emit(1);
        ray.origin = RaycastOrigin.position;
        ray.direction = RaycastDestination.position - RaycastOrigin.position;

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

    public void StopFiring()
    {
        IsFiring = false;
    }
}
