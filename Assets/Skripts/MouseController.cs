using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    [SerializeField] public int borderX = 75;
    [SerializeField] public int borderY = 75;
    [SerializeField] public int speedScroll = 10;

    [SerializeField] public float speedZoom = 10;
    [SerializeField] public float zoomStep = 1;
    [SerializeField] public float minZoomSize = 1;
    [SerializeField] public float maxZoomSize = 1;


    private List<iHighlighted> highlighted = new List<iHighlighted>();  // Список выделенных объектов

    private Camera camera;
    private float width;
    private float height;
    private float previousDist = 0;
    private Vector3 previousMousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();

        height = Screen.height;
        width = Screen.width;
    }
    private Vector2 GetTouch()
    {
        if (Input.touchCount > 0) return Input.GetTouch(0).position;
        return Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (highlighted.Count == 0)
        {
            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))  // Одно касание
            {
                previousMousePosition = camera.ScreenToWorldPoint(GetTouch(), Camera.MonoOrStereoscopicEye.Mono);
            }
            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))  // Одно касание
            {
                var currentMousePosition = camera.ScreenToWorldPoint(GetTouch(), Camera.MonoOrStereoscopicEye.Mono);
                var dist = previousMousePosition - currentMousePosition;
                transform.position += dist;
            }
        }
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))  // Одно касание
        {
            var position = camera.ScreenToWorldPoint(GetTouch(), Camera.MonoOrStereoscopicEye.Mono);

            Collider2D collider = Physics2D.OverlapPoint(position);
            if (collider != null)
            {
                iHighlighted highlighted_item = collider.gameObject.GetComponent<iHighlighted>();
                if (highlighted_item != null)
                {
                    var isContains = highlighted.Contains(highlighted_item);
                    foreach (iHighlighted e in highlighted)
                        e.unHighlight();

                    highlighted.Clear();
                    if (!isContains)
                    {
                        highlighted.Add(highlighted_item);

                        foreach (iHighlighted e in highlighted)
                            e.highlight();
                    }
                }
            }
            else
            {
                foreach (iHighlighted e in highlighted)
                {
                    e.moveTo(new Vector2(position.x, position.y));
                }
            }
        }
        if (Input.touchCount == 2)
        {
            var dist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            dist = Mathf.Abs(dist);

            if (previousDist == 0f)
                previousDist = dist;

            var newSize = camera.orthographicSize + (previousDist - dist) * speedZoom * Time.deltaTime;
            camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);

            previousDist = dist;
        }
        else if (Input.mouseScrollDelta.y > 0)  // Колёсико вверх
        {
            float newSize = camera.orthographicSize - zoomStep;
            camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        }
        else if (Input.mouseScrollDelta.y < 0)  // Колёсико вниз 
        {
            float newSize = camera.orthographicSize + zoomStep;
            camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        }
        else
        {
            previousDist = 0f;
        }
    }
}
