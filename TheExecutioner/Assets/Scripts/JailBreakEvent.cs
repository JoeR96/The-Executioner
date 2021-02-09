using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailBreakEvent : MonoBehaviour
{
    public IEnumerator OverflowEvent()
    {
        
        InvokeRepeating("Overflow",0f,0.5f);
        yield return new WaitForSeconds(2f);
        CancelInvoke();
        yield return null;
        
    }
}
