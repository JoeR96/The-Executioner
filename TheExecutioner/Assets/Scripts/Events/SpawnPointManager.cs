using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
    private List<GameObject> activeWeapons = new List<GameObject>();
    /// <summary>
    /// Select a random weapon
    /// Spawn the weapon at the spawn points
    /// set the parent
    /// add the weapon to the currently equipped weapons collection
    /// </summary>
    /// <param name="spawnPoint"></param>
    public void SpawnWeapon(Transform spawnPoint)
    {
        var rand = Random.Range(0, weapons.Count);
        var randomWeapon = weapons[rand];
        {
            var weapon = Instantiate(randomWeapon, spawnPoint.position, quaternion.identity);
            weapon.transform.SetParent(spawnPoint.transform.parent);
            activeWeapons.Add(weapon);
        }
    }
    /// <summary>
    /// Select a random weapon
    /// Spawn the weapon at the spawn points
    /// set the parent
    /// add the weapon to the currently equipped weapons collection
    /// set the weapon state to the input quality
    /// </summary>
    /// <param name="spawnPoint"></param>
    public GameObject SpawnWeapon(int quality,Transform spawnPoint)
    {
        var rand = Random.Range(0, weapons.Count);
        var randomWeapon = weapons[rand];
        var weapon = Instantiate(randomWeapon,spawnPoint.position, quaternion.identity);
        weapon.GetComponent<WeaponPickup>().SetGodBeamColour(quality);
        return weapon.gameObject;
    }
    /// <summary>
    /// Destroy all currently equipped weapons
    /// </summary>
    public void ClearWeapons()
    {
        foreach (var go in activeWeapons)
            Destroy(go);
    }
}
