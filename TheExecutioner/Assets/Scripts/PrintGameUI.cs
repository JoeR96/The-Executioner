using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PrintGameUI : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private TextMeshProUGUI currentRound;
    [SerializeField] private TextMeshProUGUI activeZombies;

    [SerializeField] private Image[] weaponIcons;
    private Dictionary<int, Color> weaponQualityColours = new Dictionary<int, Color>();

    private void Awake()
    {
        AddWeaponColours();
    }
    private void AddWeaponColours()
    {
        weaponQualityColours.Add(0,Color.green);
        weaponQualityColours.Add(1,Color.blue);
        weaponQualityColours.Add(2,Color.magenta);
        weaponQualityColours.Add(3,Color.yellow);
    }
    private void Update()
    {
        SetGameText();
    }

    private void SetGameText()
    {
        currentRound.SetText("Wave : " + roundManager.CurrentRound);
        activeZombies.SetText("Zombies alive : " + roundManager.ReturnActiveZombieCount().ToString());
    }

    public void SetWeaponColour(ActiveWeapon.WeaponSlot icon,int quality)
    {   
        weaponIcons[(int) icon].color = weaponQualityColours[quality];
    }
}
