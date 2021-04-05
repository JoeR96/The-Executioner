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
     }
     
     public void RemoveEventFromList(Event toRemove)
     {
          ActiveEventsToDisplay.Remove(toRemove);
     }
}
