using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private void OnTriggerEnter(Component other)
    {
        Debug.Log(other.name);
        Debug.Log("Entered");
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        Debug.Log(enemy);
        enemy?.IsInArea(true);

    }
    
    private void OnTriggerExit(Component other)
    {
        Debug.Log("Exit");
        var enemy = other.GetComponentInParent<IIsInEventArea>();
        Debug.Log(enemy);
        enemy?.IsInArea(false);
    }
}
