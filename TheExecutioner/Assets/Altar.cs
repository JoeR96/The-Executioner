using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyBase>())
                other.GetComponent<EnemyBase>().InEvent = true;
            Debug.Log("ZOMBIES IN ARENA!!");

    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<EnemyBase>())
            other.GetComponent<EnemyBase>().InEvent = false;
        Debug.Log("ZOMBIES LEFT ARENA!!");
    }
}
