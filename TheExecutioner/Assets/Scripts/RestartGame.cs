using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// Reload the scene to restart the game
    /// </summary>
    public void PlayGame()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// Load the main menu scene
    /// </summary>
    public void ReturnToMain()
    {
        AudioManager.Instance.PlaySound("ButtonClick");
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Quit the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
