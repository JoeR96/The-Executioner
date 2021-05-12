using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBridgeRamp : PlatformBridgeManager
{
    public bool PlatformBridgeRampActive { get; set; }
    public int CurrentBridgeRampHeight { get; set; }
    public int CurrentBridgeRampRotation  { get; set; }
    
    [SerializeField] private List<float> rampRotations = new List<float>();
    /// <summary>
    /// Toggle the bridge ramp boolean
    /// </summary>
    public override void ToggleBridge()
    {
        PlatformBridgeRampActive = !PlatformBridgeRampActive;
    }
    /// <summary>
    /// Override this to target a meshrenderer component instead of a box collider
    /// </summary>
    /// <param name="active"></param>
    public override void ActivateBridge(bool active)
    {
        bridge.GetComponentInChildren<MeshCollider>().enabled = active;
        bridge.GetComponentInChildren<MeshRenderer>().enabled = active;
    }
    /// <summary>
    /// There is a better solution here using inheritance properly
    /// However I needed to save platform information states for the bridgeramp component
    /// So using polymorphism I overrode the method to return a new property for tihs class
    /// Return the state of PlatformBridgeRampActive
    /// </summary>
    /// <returns></returns>
    public override bool ReturnBridgeValue()
    {
        return PlatformBridgeRampActive;
    }
    /// <summary>
    /// Set the ramp rotation with an index
    /// </summary>
    /// <param name="stairState"></param>
    public void SetRampRotation( int stairState )
    {
        bridge.transform.localRotation = Quaternion.Euler(bridge.
                transform.localRotation.eulerAngles.x, rampRotations[stairState],
            bridge.transform.localRotation.eulerAngles.z);
        CurrentBridgeRampRotation = stairState;
    }
}
