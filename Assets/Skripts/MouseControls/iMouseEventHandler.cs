using UnityEngine;

/// <summary>
/// Интерфейс обработчика события мышки
/// </summary>
public interface iMouseEventHandler
{
    /// <summary>
    /// Событие начала касания
    /// </summary>
    public bool touchDown(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// Событие движения касания
    /// </summary>
    public bool touchMoved(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// Событие отпускания касания
    /// </summary>
    public bool touchUp(Camera camera, Vector2 touchPosition);

    /// <summary>
    /// Событие двойного нажатия
    /// </summary>
    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch);

    /// <summary>
    /// Событие скролла мышки
    /// </summary>
    public bool mouseScroll(Camera camera, float value);
}
