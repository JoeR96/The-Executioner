using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public HealthSystem PlayerHealthSystem;
    public ActivePlayerEvents ActivePlayerEvents;
    [SerializeField] private GameObject displayEventText;
    [SerializeField] private GameObject eventScrollRect;
    
    void Awake()
    {
        
        PlayerHealthSystem = new HealthSystem(100, 100);
    }

    private void Start()
    {
        InvokeRepeating("Heal", 5f, 2.25f);
    }
    // Update is called once per frame

    private void Heal()
    {
        PlayerHealthSystem.Heal(7.5f);
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
      
        var currentEvent = other.GetComponent<IReturnEvent>();

        if (currentEvent == null) return;
        
        var activeEvent = other.GetComponent<Event>();
        ActivePlayerEvents.AddActiveEventToList(activeEvent);
        var x = Instantiate(displayEventText);
        x.transform.SetParent(eventScrollRect.transform);
        x.GetComponent<DisplayEventText>().SetEvent(activeEvent);

    }
    
    private void CheckEventRemove(Collider other)
    {
        var activeEvent = other.GetComponent<IReturnEvent>();
        if(activeEvent != null)
            ActivePlayerEvents.RemoveActiveEventFromList(activeEvent.ReturnActiveEvent());
    }
}
