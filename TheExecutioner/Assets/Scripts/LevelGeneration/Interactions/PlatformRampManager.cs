using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRampManager : MonoBehaviour
{
    public bool PlatformRampActive;
    public int CurrentRotation;
    private PlatformManager platformManager;
    [SerializeField] private List<float> rampRotations = new List<float>();
    [SerializeField] public GameObject ramp;
    

    public PlatformRampManager(PlatformManager platformStateManager)
    {
        platformManager = platformStateManager;
    }

    public void ActivateRamp(bool active)
    {
        ramp.GetComponentInChildren<MeshRenderer>().enabled = active;
        ramp.GetComponentInChildren<MeshCollider>().enabled = active;
    }

    public void SetRampRotation( int stairState)
    {
        ramp.transform.localRotation = Quaternion.Euler(ramp.
            transform.localRotation.eulerAngles.x,
            ramp.transform.localRotation.eulerAngles.y + rampRotations[stairState],
            ramp.transform.localRotation.eulerAngles.z);
        CurrentRotation = stairState;
    }

    public bool ReturnRampValue()
    {
        PlatformRampActive = !PlatformRampActive;
        return PlatformRampActive;
    }
}