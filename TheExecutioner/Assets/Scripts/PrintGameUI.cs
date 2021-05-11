using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintGameUI : MonoBehaviour
{
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private TextMeshProUGUI currentRound;
    [SerializeField] private TextMeshProUGUI activeZombies;
    private void Update()
    {
        SetGameText();
    }

    private void SetGameText()
    {
        currentRound.SetText("Wave : " + roundManager.CurrentRound.ToString());
        activeZombies.SetText("Zombies alive : " + roundManager.ReturnActiveZombieCount().ToString());
    }
}
