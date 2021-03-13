using UnityEngine;

public class PlatformColourManager : MonoBehaviour
{
    private PlatformManager platformManager;
    public Material[] materials;
    public bool ColourTileMode;
    public bool ColourAdjacentMode;
    public int CurrentColour;

    public PlatformColourManager(PlatformManager platformStateManager)
    {
        platformManager = platformStateManager;
    }

    public void SetAdjacentColour(int materialIndex)
    {
        var adjacent = GameManager.instance.EnvironmentManager.environmentSpawner.CheckAdjacentClosePositions(platformManager.PlatformStateManager.Node);
        foreach (var go in adjacent)
        {
            go.platform.GetComponent<MeshRenderer>().material = materials[materialIndex];
            go.PlatformManager.PlatformColourManager.CurrentColour = materialIndex;
            go.PlatformManager.PlatformColourManager.
                //go.PlatformState.ChangeMaterial(materialIndex);
                CurrentColour = materialIndex;
        }
    }

    public void SetPlatformColour(int materialIndex)
    {
        GetComponent<MeshRenderer>().material = materials[materialIndex];
    }
}