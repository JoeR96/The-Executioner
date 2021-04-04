using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HealthSystem PlayerHealthSystem;
    public EventManager EventManager;
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
    
    private void OnTriggerEnter(Component other)
    {
        CheckForActiveEvent(other);
    }
    
    private void OnTriggerExit(Component other)
    {
        CheckEventRemove(other);
    }
    
    private void CheckForActiveEvent(Component other)
    {
        var returnEvent = other.GetComponent<IReturnEvent>();
        var activeEvent = returnEvent?.ReturnActiveEevent();
        EventManager.AddActiveEventToList(activeEvent);
    }
    
    private void CheckEventRemove(Component other)
    {
        var returnEvent = other.GetComponent<IReturnEvent>();
        var activeEvent = returnEvent?.ReturnActiveEevent();
        EventManager.RemoveActiveEventFromList(activeEvent);
    }
}
