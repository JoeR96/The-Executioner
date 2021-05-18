using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate( new Vector3(0, 1f,0) );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var character = other.GetComponent<CharacterManager>();
            character.PlayerHealthSystem.Heal(100);
            Destroy(gameObject);
        }
    }
}
