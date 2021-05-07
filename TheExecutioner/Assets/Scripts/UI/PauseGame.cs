using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PauseGame : MonoBehaviour
{
    private void Update()
    {
        
    }
    /// <summary>
    /// Pause the game by setting setting timescale to 0
    /// Display the cursor
    /// Enable the pause canvas
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }
    /// <summary>
    /// Unpause the game by setting setting timescale to 1
    /// Disable the cursor
    /// Disable the pause canvas
    /// </summary>
    private void Resume()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
