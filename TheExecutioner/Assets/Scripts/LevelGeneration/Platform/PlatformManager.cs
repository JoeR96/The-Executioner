using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public PlatformRampManager PlatformRampManager { get; private set; }
    public PlatformBridgeManager PlatformBridgeManager { get; private set; }
    public PlatformColourManager PlatformColourManager { get; private set; }
    public PlatformHeightManager PlatformHeightManager { get; private set; }
    public PlatformSpawnManager PlatformSpawnManager { get; private set;}
    public PlatformStateManager PlatformStateManager { get; private set;}

    private void Awake()
    {
        PlatformRampManager = GetComponent<PlatformRampManager>();
        PlatformBridgeManager = GetComponent<PlatformBridgeManager>();
        PlatformColourManager = GetComponent<PlatformColourManager>();
        PlatformHeightManager = GetComponent<PlatformHeightManager>();
        PlatformSpawnManager = GetComponent<PlatformSpawnManager>();
        PlatformStateManager = GetComponent<PlatformStateManager>();
    }
}