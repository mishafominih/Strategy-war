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

    public bool touchUp(Camera camera, Vector2 touchPosition)
    {
        var position = camera.ScreenToWorldPoint(touchPosition, Camera.MonoOrStereoscopicEye.Mono);

        Collider2D collider = Physics2D.OverlapPoint(position);
        if (collider != null)
        {
            iHighlighted highlighted_item = collider.gameObject.GetComponent<iHighlighted>();
            if (highlighted_item != null)
            {
                if (highlighted.Count > 0)
                {
                    var isContains = highlighted.Contains(highlighted_item);
                    foreach (iHighlighted e in highlighted)
                        e.unHighlight();

                    EventsManager.eventUnHighlight.Invoke();
                    highlighted.Clear();

                    if (isContains)
                        return false;
                }

                highlighted.Add(highlighted_item);
                EventsManager.eventHighlight.Invoke(highlighted_item);

                foreach (iHighlighted e in highlighted)
                    e.highlight();

                return true;
            }
        }
        else
        {
            foreach (iHighlighted e in highlighted)
            {
                e.moveTo(new Vector2(position.x, position.y));
                e.unHighlight();
            }
            EventsManager.eventUnHighlight.Invoke();
            highlighted.Clear();
            return false;
        }
        return false;
    }

    public bool touchDown(Camera camera, Vector2 touchPosition)
    {
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
