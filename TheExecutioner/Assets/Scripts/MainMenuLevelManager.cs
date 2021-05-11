using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLevelManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MainMenuSequence",1f,4f);
    }

    private void MainMenuSequence()
    {
        levelManager.LoadLevel();
        levelManager.LoadStage();
    }
}
