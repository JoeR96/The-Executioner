using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool IsFiring;
    public Transform RaycastOrigin;
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitEffect;
    public TrailRenderer TracerEffect;
    public Transform RaycastDestination;
    private Ray ray;
    private RaycastHit hitInfo;
    
    
    public void StartFiring()
    {
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

            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        IsFiring = false;
    }
}
