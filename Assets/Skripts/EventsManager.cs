using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    //  UI События взаимодействия с объектами
    public static UnityEvent<List<iHighlighted>> eventTouchHighlighted = new UnityEvent<List<iHighlighted>>();  // Событие выбора войск
    public static UnityEvent<Vector2, Vector2> eventTouchEmpty = new UnityEvent<Vector2, Vector2>();  // Кликнули в пустоту. Точка начала касания и точка окончания касания
    public static UnityEvent<iHighlighted> eventHighlight = new UnityEvent<iHighlighted>();  // Выделение войска
    public static UnityEvent<iHighlighted> eventUnHighlight = new UnityEvent<iHighlighted>();  // Снятие выделения

    // События боевой системы
    public static UnityEvent<iButtleUnit> eventButtleUnitDie = new UnityEvent<iButtleUnit>();  // Событие выбора войск
}
