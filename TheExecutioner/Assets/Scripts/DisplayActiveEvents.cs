using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayActiveEvents : MonoBehaviour
{
     public List<Event> ActiveEventsToDisplay { get; set; }

     public void DrawEventToScreen(Event eventToDraw)
     {
          
     }
     public void AddActiveEventToList(Event newEvent)
     {
          ActiveEventsToDisplay.Add(newEvent);
          Debug.Log(newEvent.progress);
     }
     
     public void RemoveEventFromList(Event toRemove)
     {
          ActiveEventsToDisplay.Remove(toRemove);
          Debug.Log(toRemove.progress);
     }
}
