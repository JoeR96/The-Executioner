using UnityEngine;

public class WeaponStateManager : MonoBehaviour
{
    private RaycastWeapon _raycastWeapon;

    private void Start()
    {
        _raycastWeapon = GetComponent<RaycastWeapon>();
    }
    

}