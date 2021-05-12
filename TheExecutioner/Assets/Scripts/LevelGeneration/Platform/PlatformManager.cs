using UnityEngine;

public class PlatformManager : MonoBehaviour
{
 
    /// <summary>
    /// These values need to be serialized for the custom inspector to work appropriately
    /// [field:SerializeField] is used to allow properties to be serialized
    /// </summary>
    /// 
    [field:SerializeField] public PlatformRampManager PlatformRampManager { get;  set; }
    [field:SerializeField] public PlatformBridgeManager PlatformBridgeManager { get; set; }
    [field:SerializeField] public PlatformBridgeRamp PlatformBridgeRampManager { get; set; }
    [field:SerializeField] public PlatformColourManager PlatformColourManager { get; set;  }
    [field:SerializeField] public PlatformHeightManager PlatformHeightManager { get; set; }
    [field:SerializeField] public PlatformSpawnManager PlatformSpawnManager { get; set; }
    [field:SerializeField]  public PlatformStateManager PlatformStateManager { get;set; }

    public void ResetPlatformStates()
    {
        
       
    }
    
}