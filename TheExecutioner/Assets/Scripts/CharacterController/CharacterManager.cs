using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HealthSystem PlayerHealthSystem;
    public ActivePlayerEvents ActivePlayerEvents;
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
    
    private void OnTriggerEnter(Collider other)
    {
        CheckForActiveEvent(other);
    }
    
    private void OnTriggerExit(Collider other)
    {
        CheckEventRemove(other);
    }
    
    private void CheckForActiveEvent(Collider other)
    {
        var activeEvent = other.GetComponent<IReturnEvent>();
        if (activeEvent != null)
        {
            Debug.Log(activeEvent);
            Debug.Log("TESTICLES");
        }
        if(activeEvent != null)
            ActivePlayerEvents.AddActiveEventToList(activeEvent.ReturnActiveEevent());
    }
    
    private void CheckEventRemove(Collider other)
    {
        var activeEvent = other.GetComponent<IReturnEvent>();
        if(activeEvent != null)
            ActivePlayerEvents.RemoveActiveEventFromList(activeEvent.ReturnActiveEevent());
    }
}
