using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public float distance = 10f; // Distance from the camera to the point in the world

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distance; // Set the distance from the camera

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.LookAt(worldPosition);
    }
}
