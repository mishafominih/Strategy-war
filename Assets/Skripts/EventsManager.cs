using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    //  UI События взаимодействия с объектами
    public static UnityEvent<iHighlighted> eventTouchHighlighted = new UnityEvent<iHighlighted>();  // Событие выбора войск
    public static UnityEvent<Vector2> eventTouchEmpty = new UnityEvent<Vector2>();  // Событие выбора войск
    public static UnityEvent<iHighlighted> eventHighlight = new UnityEvent<iHighlighted>();  // Выделение войска
    public static UnityEvent<iHighlighted> eventUnHighlight = new UnityEvent<iHighlighted>();  // Снятие выделения

    // События боевой системы
    public static UnityEvent<iButtleUnit> eventButtleUnitDie = new UnityEvent<iButtleUnit>();  // Событие выбора войск
}
