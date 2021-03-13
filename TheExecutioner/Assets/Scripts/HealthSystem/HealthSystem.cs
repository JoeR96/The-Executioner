using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
 private float currentHealth;
 private float maxHealth;
 private bool isCollective;
 private int collectivePlace;

 public float CurrentHealth
 {
  get => currentHealth;
  set => currentHealth = value;
 }


    public bool IsCollective    {
        get => isCollective;
        set => isCollective = value;
    }

    public int CollectivePlace
    {
        get => collectivePlace;
        //set => collectivePlace = value;
    }





    public HealthSystem(float current, float max)
 {
        currentHealth = current;
        maxHealth = max;
        isCollective = false;
 }

    public HealthSystem(float current, float max, int place)
    {
        currentHealth = current;
        maxHealth = max;
        isCollective = true;
        collectivePlace = place;
    }
    private void Awake()
 {
  currentHealth = maxHealth;
 }
 
 public float MaxHealth
 {
  set => maxHealth = value;
 }
 
 public bool TakeDamage(float damage)
 {
  currentHealth -= damage;
  return currentHealth > 0;
 }
 
     public void Heal(float heal)
     {
      currentHealth = Mathf.Clamp(currentHealth += heal, 0, max: maxHealth);
     }

    public bool BelowPercent(int percent)
    {
        if(currentHealth / maxHealth * 100 <= percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
