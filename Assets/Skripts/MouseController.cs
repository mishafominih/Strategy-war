using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
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
            Debug.Log(startPosition.x);
            Debug.Log(startPosition.y);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Camera camera = GetComponent<Camera>();
            endPosition = camera.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            Debug.Log(endPosition.x);
            Debug.Log(endPosition.y);

            endPosition.z = camera.nearClipPlane;
            Vector2 mouseWorldPosition = camera.ScreenToWorldPoint(endPosition);

            Collider2D detectedCollider =
                Physics2D.OverlapBox(mouseWorldPosition, detectionBoxSize, rotationAngle);
        }
    }
}
