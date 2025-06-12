using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private List<iHighlighted> highlighted = new List<iHighlighted>();  // Список выделенных объектов
    private Vector3 startPosition;
    private Vector3 endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Camera camera = GetComponent<Camera>();
            startPosition = camera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            foreach (iHighlighted e in highlighted)
                e.unHighlight();

            highlighted.Clear();

            Camera camera = GetComponent<Camera>();
            endPosition = camera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);

            Collider2D collider = Physics2D.OverlapPoint(endPosition);
            if (collider != null)
            {
                iHighlighted highlighted_item = collider.gameObject.GetComponent<iHighlighted>();
                if (highlighted_item != null)
                {
                    highlighted.Add(highlighted_item);

                    foreach (iHighlighted e in highlighted)
                        e.highlight();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Camera camera = GetComponent<Camera>();
            Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            foreach (iHighlighted e in highlighted)
            {
                e.moveTo(new Vector2(position.x, position.y));
            }
                
        }
    }
}
