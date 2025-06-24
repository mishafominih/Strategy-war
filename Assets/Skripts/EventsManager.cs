using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager
{
    public static UnityEvent<iHighlighted> eventTouchHighlighted = new UnityEvent<iHighlighted>();  // ������� ������ �����
    public static UnityEvent<Vector2> eventTouchEmpty = new UnityEvent<Vector2>();  // ������� ������ �����
    public static UnityEvent<iHighlighted> eventHighlight = new UnityEvent<iHighlighted>();  // ��������� ������
    public static UnityEvent<iHighlighted> eventUnHighlight = new UnityEvent<iHighlighted>();  // ������ ���������
}
