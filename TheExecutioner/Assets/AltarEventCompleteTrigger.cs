using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarEventCompleteTrigger : MonoBehaviour
{
    [field: SerializeField]
    public Transform HeartTargetPosition
    {
        get; private set;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            var heart = other.GetComponent<Heart>();
            other.GetComponent<HeartEscortEvent>().EventComplete(heart);
        }
    }
}
