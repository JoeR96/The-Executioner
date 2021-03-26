using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HealthSystem PlayerHealthSystem;
    
    void Awake()
    {
        PlayerHealthSystem = new HealthSystem(100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage )
    {
        if(!PlayerHealthSystem.TakeDamage(damage))
         GameManager.instance.GameOver();
    }
}
