using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformStateManager : MonoBehaviour
{
    
    public bool PlatformIsWall;
    public bool PlatformIsActive = false;
    
    private PlatformManager platformManager;
    
    //I need to figure out where to put these variables
    [SerializeField] private int boundarySize;
    public int X;
    public int Z;
    public Node Node;
    public void SetNode(Node node)
    {
        Node = node;
    }
    public void Setint(int x, int z)
    {
        X = x;
        Z = z;
    }

    private void Awake()
    {
        platformManager = GetComponent<PlatformManager>();
    }
    
    public void SetStateFromExternal(PlatformInformation platformInformation)
    {
        platformManager.PlatformBridgeManager.PlatformBridgeActive = platformInformation.BridgeIsActive;
        platformManager.PlatformRampManager.PlatformRampActive = platformInformation.PlatformStairActive;
        platformManager.PlatformHeightManager.CurrentHeight = platformInformation.CurrentHeight;
        platformManager.PlatformBridgeManager.CurrentBridgeHeight = platformInformation.CurrentBridgeHeight;
        platformManager.PlatformRampManager.CurrentRotation = platformInformation.CurrentRotation;
        platformManager.PlatformSpawnManager.PlatformSpawnPointActive = platformInformation.PlatformSpawnActive;
        platformManager.PlatformSpawnManager.PlatformEventSpawn = platformInformation.PlatformEventSpawn;

        SetState();
    }
    public void SetState()
    {
        platformManager.PlatformHeightManager.SetPlatformHeight(platformManager.PlatformHeightManager.CurrentHeight);
        platformManager.PlatformRampManager.SetRampRotation(platformManager.PlatformRampManager.CurrentRotation);
        platformManager.PlatformColourManager.SetPlatformColour(platformManager.PlatformColourManager.CurrentColour);
        platformManager.PlatformRampManager.ActivateRamp(platformManager.PlatformRampManager.PlatformRampActive);
        platformManager.PlatformBridgeManager.ActivateBridge(platformManager.PlatformBridgeManager.PlatformBridgeActive);
        //platformManager.PlatformBridgeManager.SetBridgeHeight(platformManager.PlatformBridgeManager.CurrentBridgeHeight);
        platformManager.PlatformSpawnManager.ActivateSpawnPoint(platformManager.PlatformSpawnManager.PlatformSpawnPointActive);


    }
    public void ResetState()
    {
        
        platformManager.PlatformBridgeManager.PlatformBridgeActive = false;
        platformManager.PlatformBridgeManager.SetBridgeHeight((int) PlatformBridgeHeight.Disabled);
        platformManager.PlatformBridgeManager.ActivateBridge(false);
        platformManager.PlatformHeightManager.SetPlatformHeight((int) PlatformHeight.Flat);
        platformManager.PlatformRampManager.PlatformRampActive = false;
        platformManager.PlatformRampManager.SetRampRotation(0);
        platformManager.PlatformRampManager.ActivateRamp(false);
     
        
    }
    
    
    //spawnpoint

    //manager
  


   
}


