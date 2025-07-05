using UnityEngine;

/// <summary>
/// Обработчик на изменение масштаба
/// </summary>
public class CameraSizeController : iMouseEventHandler
{
    private float speedZoom = 0.2f;
    private float previousDist = 0f;

    public float minZoomSize = 2;
    public float maxZoomSize = 10;

    private GameObject backGround;
    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch)
    {
        var dist = Vector2.Distance(firstTouch, secondTouch);
        dist = Mathf.Abs(dist);

        if (previousDist == 0f)
            previousDist = dist;

        var newSize = camera.orthographicSize + (previousDist - dist) * speedZoom * Time.deltaTime;
        SetSize(camera, newSize);

        previousDist = dist;
        return false;
    }

    private void SetSize(Camera camera, float newSize)
    {
        var prevCameraSize = camera.orthographicSize;
        camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        var newCameraSize = camera.orthographicSize;

        var diff = newCameraSize / prevCameraSize;

        var backGround = getBackGround();
        if (backGround != null)
        {
            backGround.transform.localScale = new Vector3(
                backGround.transform.localScale.x * diff,
                backGround.transform.localScale.y * diff,
                backGround.transform.localScale.z * diff
            );
        }
    }

    public bool mouseScroll(Camera camera, float value)
    {
        float newSize = camera.orthographicSize - value;
        SetSize(camera, newSize);
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

    private GameObject getBackGround()
    {
        return backGround is null ? GameObject.FindGameObjectWithTag("BackGround") : backGround;
    }
}
