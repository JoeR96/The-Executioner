using UnityEngine;

public class PlatformColourManager : MonoBehaviour
{
    private PlatformManager platformManager;
    public Material[] materials;
    public bool ColourTileMode;
    public bool ColourAdjacentMode;
    public int CurrentColour;

    private CheckAdjacentNodes CheckAdjacentNodes;

    private void Start()
    {
        CheckAdjacentNodes = new CheckAdjacentNodes();
        platformManager = GetComponent<PlatformManager>();
    }


    public void SetAdjacentColour(int materialIndex)
    {
        var adjacent = CheckAdjacentNodes.CheckAdjacentClosePositions(platformManager.PlatformStateManager.Node,GameManager.instance.EnvironmentManager.grid.grid);
        
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