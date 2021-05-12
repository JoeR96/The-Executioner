using UnityEngine;

public class PlatformSpawnManager : MonoBehaviour
{
    public bool PlatformSpawnPointActive;
    public bool PlatformEventSpawn;
    [SerializeField] public GameObject spawnPoint;


    /// <summary>
    /// Activate the enemy spawn point from editor
    /// </summary>
    public void ActivateSpawnPoint()
    {
        PlatformSpawnPointActive = true;
    }
    /// <summary>
    /// Deactivate enemy spawn point from editor
    /// </summary>
    public void DeactivateSpawnPoint()
    {
        PlatformSpawnPointActive = false;
    }
    /// <summary>
     /// Activate the event spawn from the editor
     /// </summary>
    public void ActivateEventSpawnPoint()
    {
        PlatformEventSpawn = true;
    }
    /// <summary>
    /// Deactivate event spawn point from editor
    /// </summary>
    public void DeactivateEventSpawnPoint()
    {
        PlatformEventSpawn = false;
    }
    public bool ReturnPlatformSpawnPointValue()
    {
        return PlatformSpawnPointActive;
    }
    public bool ReturnPlatformEventSpawnPointValue()
    {
        return PlatformEventSpawn;
    }

    public void ActivateSpawnPoint(bool active)
    {
        
    }
}