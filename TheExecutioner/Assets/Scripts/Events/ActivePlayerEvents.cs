using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerEvents : MonoBehaviour
{
    public List<Event> ActiveEvents;
    public List<Event> InRangeEvent;
    public void AddEventToList(Event newEvent)
    {
        ActiveEvents.Add(newEvent);
    }

    public void RemoveEventFromList(Event eventToRemove)
    {
        ActiveEvents.Remove(eventToRemove);
    }
    
    public void AddActiveEventToList(Event newEvent)
    {
        InRangeEvent.Add(newEvent);
    }

    public void RemoveActiveEventFromList(Event eventToRemove)
    {
        InRangeEvent.Remove(eventToRemove);
    }

    public virtual void DisplayEvent(Event eventToDisplay)
    {
        
    }
}
