using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RaycastWeapon
{
    public Shotgun()
    {
        fireWeapon = "ShotgunFire";
        reloadWeapon = "ShotgunReload";
    }
}