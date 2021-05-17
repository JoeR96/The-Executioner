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
    bool over;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart") && over == false)
        {
            over = true;
            var heart = other.GetComponent<Heart>();
            other.GetComponent<HeartEscortEvent>().EventComplete(heart);
        }
    }
}
