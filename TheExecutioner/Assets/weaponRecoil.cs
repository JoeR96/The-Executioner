using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class weaponRecoil : MonoBehaviour
{
    [HideInInspector]public CinemachineFreeLook Playercamera;
    [HideInInspector] public CinemachineImpulseSource CameraShake;
    [HideInInspector] public Animator RigController;
    public Vector2[] RecoilPattern;
    public float duration;
    
    private float verticalRecoil;
    private float horizontalRecoil;
    private float time;
    private int _index;
    private int NextIndex(int index)
    {
        return (index + 1) % RecoilPattern.Length;
    }
    private void Awake()
    {
        CameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void Reset()
    {
        _index = 0;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        CameraShake.GenerateImpulse(Camera.main.transform.forward);
        horizontalRecoil = RecoilPattern[_index].x;
        verticalRecoil = RecoilPattern[_index].y;
        _index = NextIndex(_index);
        
        RigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }

    private void Update()
    {
        if (time > 0)
        {
            Playercamera.m_YAxis.Value -= ((verticalRecoil/1000)* Time.deltaTime) / duration;
            Playercamera.m_XAxis.Value -= ((horizontalRecoil/10)* Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}

