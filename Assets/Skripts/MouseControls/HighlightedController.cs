using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Обработчик на управление выделенными объектами
/// </summary>
public class HighlightedController : iMouseEventHandler
{
    private List<iHighlighted> highlighted = new List<iHighlighted>();  // Список выделенных объектов
    private Vector2 startTouchPosition;
    public HighlightedController()
    {
        EventsManager.eventHighlight.AddListener(eventHighltghtedHandler);
        EventsManager.eventUnHighlight.AddListener(eventUnHighltghtedHandler);
    }


    private void eventHighltghtedHandler(iHighlighted highlightedItem)
    {
        highlighted.Add(highlightedItem);
    }

    private void eventUnHighltghtedHandler(iHighlighted highlightedItem)
    {
        highlighted.Remove(highlightedItem);
    }

    public bool touchUp(Camera camera, Vector2 touchPosition)
    {
        var position = camera.ScreenToWorldPoint(touchPosition, Camera.MonoOrStereoscopicEye.Mono);

        Collider2D[] colliders = Physics2D.OverlapPointAll(position);
        if (colliders != null && colliders.Length > 0)
        {
            List<iHighlighted> highlighteds = new List<iHighlighted>();
            foreach (var collider in colliders)
            {
                iHighlighted highlighted_item = collider.gameObject.GetComponent<iHighlighted>();
                if (highlighted_item != null)
                    highlighteds.Add(highlighted_item);
            }
            if (highlighteds.Count > 0)
            {
                EventsManager.eventTouchHighlighted.Invoke(highlighteds);
            }
        }
        else
        {
            EventsManager.eventTouchEmpty.Invoke(startTouchPosition, position);
        }
        return highlighted.Count > 0;
    }

    public bool touchDown(Camera camera, Vector2 touchPosition)
    {
        startTouchPosition = camera.ScreenToWorldPoint(touchPosition, Camera.MonoOrStereoscopicEye.Mono);
        return highlighted.Count > 0;
    }

    public bool touchMoved(Camera camera, Vector2 touchPosition)
    {
        return highlighted.Count > 0;
    }

    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch)
    {
        return highlighted.Count > 0;
    }

    public bool mouseScroll(Camera camera, float value)
    {
        return highlighted.Count > 0;
    }
}
