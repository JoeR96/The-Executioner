using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour
{
    public bool PlatformSpawnPointActive;
    public bool PlatformEventSpawn;
    [SerializeField] public GameObject spawnPoint;
    

  

    public bool ReturnPlatformSpawnPointValue()
    {
        PlatformSpawnPointActive = !PlatformSpawnPointActive;
        return PlatformSpawnPointActive;
    }
    public bool ReturnPlatformEventSpawnPointValue()
    {
        PlatformEventSpawn = !PlatformEventSpawn;
        return PlatformEventSpawn;
    }

    public void ActivateSpawnPoint(bool active)
    {
        
    }
}