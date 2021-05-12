using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformStateManager : MonoBehaviour
{
    
    public bool PlatformIsPlayable;
    public bool PlatformIsActive = false;
    
    private PlatformManager platformManager;
    [SerializeField] private int boundarySize;
    public int X;
    public int Z;
    public Node Node;
    
    /// <summary>
    /// set the platform node
    /// </summary>
    /// <param name="node"></param>
    public void SetNode(Node node)
    {
        Node = node;
    }
    /// <summary>
    /// set the X and Z coordinate of the platform
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }
    private void Awake()
    {
        platformManager = GetComponent<PlatformManager>();
    }
    /// <summary>
    /// Use the serialized data within platformInformation to set the relevant index within the platform manager below
    /// </summary>
    /// <param name="platformInformation"></param>
    public void SetStateFromExternal(PlatformInformation platformInformation)
    {
        platformManager.PlatformBridgeManager.PlatformBridgeActive = platformInformation.BridgeIsActive;
        platformManager.PlatformRampManager.PlatformRampActive = platformInformation.PlatformStairActive;
        platformManager.PlatformBridgeRampManager.PlatformBridgeRampActive = platformInformation.PlatformBridgeRampActive;
        platformManager.PlatformBridgeRampManager.CurrentBridgeRampHeight = platformInformation.CurrentBridgeRampHeight;
        platformManager.PlatformBridgeRampManager.CurrentBridgeRampRotation = platformInformation.CurrentBridgeRampRotation;
        platformManager.PlatformHeightManager.CurrentHeight = platformInformation.CurrentHeight;
        platformManager.PlatformBridgeManager.CurrentBridgeHeight = platformInformation.CurrentBridgeHeight;
        platformManager.PlatformRampManager.CurrentRotation = platformInformation.CurrentRotation;
        platformManager.PlatformSpawnManager.PlatformSpawnPointActive = platformInformation.PlatformSpawnActive;
        platformManager.PlatformSpawnManager.PlatformEventSpawn = platformInformation.PlatformEventSpawn;

        SetState();
    }
    /// <summary>
    /// using previously input information from platformInformation set the status of each component to the previously applied index
    /// </summary>
    private void SetState()
    {
        platformManager.PlatformHeightManager.SetPlatformHeight(platformManager.PlatformHeightManager.CurrentHeight);
        platformManager.PlatformRampManager.SetRampRotation(platformManager.PlatformRampManager.CurrentRotation);
        platformManager.PlatformColourManager.SetPlatformColour(platformManager.PlatformColourManager.CurrentColour);
        platformManager.PlatformRampManager.ActivateRamp(platformManager.PlatformRampManager.PlatformRampActive);
        platformManager.PlatformBridgeManager.ActivateBridge(platformManager.PlatformBridgeManager.PlatformBridgeActive);
        platformManager.PlatformBridgeManager.SetBridgeHeight(platformManager.PlatformBridgeManager.CurrentBridgeHeight);
        platformManager.PlatformBridgeRampManager.ActivateBridge(platformManager.PlatformBridgeRampManager.PlatformBridgeRampActive);
        platformManager.PlatformBridgeRampManager.SetBridgeHeight(platformManager.PlatformBridgeRampManager.CurrentBridgeRampHeight);
        platformManager.PlatformBridgeRampManager.SetRampRotation(platformManager.PlatformBridgeRampManager.CurrentBridgeRampRotation);
        platformManager.PlatformSpawnManager.ActivateSpawnPoint(platformManager.PlatformSpawnManager.PlatformSpawnPointActive);
    }
    /// <summary>
    /// reset the platform components to their original status
    /// </summary>
    public void ResetState()
    {
        platformManager.PlatformRampManager.ActivateRamp(false);
        platformManager.PlatformBridgeManager.ActivateBridge(false);
        platformManager.PlatformBridgeRampManager.ActivateBridge(false);
        platformManager.PlatformHeightManager.ResetPlatformHeight();
        platformManager.PlatformSpawnManager.DeactivateSpawnPoint();
        platformManager.PlatformSpawnManager.DeactivateEventSpawnPoint();
        platformManager.PlatformStateManager.ResetState();
    }
}


