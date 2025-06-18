using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    public static UnityEvent<iHighlighted> eventHighlight = new UnityEvent<iHighlighted>();
    public static UnityEvent eventUnHighlight = new UnityEvent();
}
