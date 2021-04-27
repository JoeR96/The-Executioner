using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedWeapon : MonoBehaviour
{
    [SerializeField] private Image[] weapons = new Image[3];
    
    public int CurrentWeapon { get; set; }

    
    public void DisplayEquippedWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
             var newColour = weapons[i].color;
             weapons[i].color = new Color(newColour.r, newColour.g, newColour.b, 0.25f);
            if (i == index)
                weapons[i].color = new Color(newColour.r, newColour.g, newColour.b, 1f);
        }
    }
}
