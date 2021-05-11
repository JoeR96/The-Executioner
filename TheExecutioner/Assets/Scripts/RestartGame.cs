using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// Reload the scene to restart the game
    /// </summary>
    public void Restart()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Quit t he application
    /// </summary>
    public void Quit()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        Application.Quit();
    }
}
