﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        AudioManager.Instance.PlaySound("ButtonClick");
    }

    public void Quit()
    {
        Application.Quit();
        AudioManager.Instance.PlaySound("ButtonClick");
    }
}