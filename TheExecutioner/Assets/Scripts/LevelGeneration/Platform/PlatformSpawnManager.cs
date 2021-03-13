using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour
{
    public bool PlatformSpawnPointActive;
    [SerializeField] public GameObject spawnPoint;

    public PlatformSpawnManager(PlatformManager platformManager)
    {
    }

    public bool ReturnPlatformSpawnPointValue()
    {
        PlatformSpawnPointActive = !PlatformSpawnPointActive;
        return PlatformSpawnPointActive;
    }

    public void ActivateSpawnPoint(bool active)
    {
        
    }
}