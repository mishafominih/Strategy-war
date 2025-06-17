using UnityEngine;

/// <summary>
/// Обработчик на изменение масштаба
/// </summary>
public class CameraSizeController : iMouseEventHandler
{
    private float speedZoom = 0.2f;
    private float previousDist = 0f;

    public float minZoomSize = 2;
    public float maxZoomSize = 5;
    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch)
    {
        var dist = Vector2.Distance(firstTouch, secondTouch);
        dist = Mathf.Abs(dist);

        if (previousDist == 0f)
            previousDist = dist;

        var newSize = camera.orthographicSize + (previousDist - dist) * speedZoom * Time.deltaTime;
        camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);

        previousDist = dist;
        return false;
    }

    public bool mouseScroll(Camera camera, float value)
    {
        float newSize = camera.orthographicSize - value;
        camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        return false;
    }

    public bool touchDown(Camera camera, Vector2 touchPosition)
    {
        previousDist = 0f;
        return false;
    }

    public bool touchMoved(Camera camera, Vector2 touchPosition)
    {
        return false;
    }

    public bool touchUp(Camera camera, Vector2 touchPosition)
    {
        return false;
    }
}
