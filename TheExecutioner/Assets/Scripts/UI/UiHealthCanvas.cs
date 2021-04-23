using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthCanvas : MonoBehaviour
{
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Image playerProgressBar;

    private void Update()
    {
        playerProgressBar.color = new Color(playerProgressBar.color.r, playerProgressBar.color.g,
            playerProgressBar.color.b, characterManager.PlayerHealthSystem.Percent());
    }
    

}
