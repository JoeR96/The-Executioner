using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEscortEvent : MonoBehaviour
{
    [SerializeField] private Heart heart;
    
    public void StartEvent(Transform altarTransform)
    {
        var target = altarTransform;
        heart.SetTargetPosition(target);
    }
    
}
