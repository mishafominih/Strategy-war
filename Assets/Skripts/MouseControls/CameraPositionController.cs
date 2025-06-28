using UnityEngine;

/// <summary>
/// Обработчик на изменение местоположения камеры
/// </summary>
public class CameraPositionController : iMouseEventHandler
{
    private Vector3 previousMousePosition;
    private GameObject map;

    public bool touchDown(Camera camera, Vector2 touchPosition)
    {
        previousMousePosition = camera.ScreenToWorldPoint(touchPosition, Camera.MonoOrStereoscopicEye.Mono);
        return false;
    }

    public bool touchMoved(Camera camera, Vector2 touchPosition)
    {
        var currentMousePosition = camera.ScreenToWorldPoint(touchPosition, Camera.MonoOrStereoscopicEye.Mono);
        var dist = previousMousePosition - currentMousePosition;
        camera.transform.position += dist;

        var map = getMap().GetComponent<SpriteRenderer>();
        var isContains = map.bounds.Contains(
            new Vector3(
                camera.transform.position.x,
                camera.transform.position.y,
                map.transform.position.z
        ));
        if (!isContains)
        {
            camera.transform.position -= dist;
        }

        return false;
    }

    public bool touchUp(Camera camera, Vector2 touchPosition)
    {
        return false;
    }

    public bool doubleTouch(Camera camera, Vector2 firstTouch, Vector2 secondTouch)
    {
        return false;
    }

    public bool mouseScroll(Camera camera, float value)
    {
        return false;
    }

    private GameObject getMap()
    {
        return map is null ? GameObject.FindGameObjectWithTag("Map") : map;
    }
}
