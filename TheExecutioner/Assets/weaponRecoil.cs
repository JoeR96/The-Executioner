using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class weaponRecoil : MonoBehaviour
{
    [HideInInspector]public CinemachineFreeLook Playercamera;
    [HideInInspector] public CinemachineImpulseSource CameraShake;
    public float verticalRecoil;
    public float horizontalRecoil;
    public float duration;

    private float time;

    private void Awake()
    {
        CameraShake = GetComponent<CinemachineImpulseSource>();
    }
    public void GenerateRecoil()
    {
        time = duration;
        CameraShake.GenerateImpulse(Camera.main.transform.forward);  
    }

    private void Update()
    {
        if (time > 0)
        {
            Playercamera.m_YAxis.Value -= ((verticalRecoil/1000  )* Time.deltaTime) / duration;
            Playercamera.m_XAxis.Value -= ((horizontalRecoil/10  )* Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}

