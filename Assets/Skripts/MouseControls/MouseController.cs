using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseController : MonoBehaviour
{
    [SerializeField] public float speedZoom = 10;
    [SerializeField] public float zoomStep = 1;
    [SerializeField] public float minZoomSize = 1;
    [SerializeField] public float maxZoomSize = 1;

    private Camera camera;
    private iMouseEventHandler mouseEventHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GetComponent<Camera>();
        mouseEventHandler = new MouseEventController();
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))  // Одно касание
        {
            mouseEventHandler.touchDown(camera, GetTouch());
        }
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))  // Одно касание
        {
            mouseEventHandler.touchMoved(camera, GetTouch());
        }
        if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))  // Одно касание
        {
            mouseEventHandler.touchUp(camera, GetTouch());
        }
        if (Input.touchCount == 2)
        {
            mouseEventHandler.doubleTouch(camera, Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
        if (Input.mouseScrollDelta.y > 0)  // Колёсико вверх
        {
            mouseEventHandler.mouseScroll(camera, Input.mouseScrollDelta.y);
        }
        if (Input.mouseScrollDelta.y < 0)  // Колёсико вниз 
        {
            mouseEventHandler.mouseScroll(camera, Input.mouseScrollDelta.y);
        }
    }
    private Vector2 GetTouch()
    {
        if (Input.touchCount > 0) return Input.GetTouch(0).position;
        return Input.mousePosition;
    }

}
