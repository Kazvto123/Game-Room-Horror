using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float edgeSize = 50f; // Size of the edge area
    public float moveSpeed = 5f; // Speed of movement
    public float maxHorizontalPosition = 10f; // Maximum horizontal position
    public float minHorizontalPosition = -10f; // Minimum horizontal position
    public float maxVerticalPosition = 10f; // Maximum vertical position
    public float minVerticalPosition = -10f; // Minimum vertical position

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector3 movement = Vector3.zero;

        // Check if the mouse is near the left edge and within limits
        if (mousePosition.x < edgeSize && transform.position.x > minHorizontalPosition)
        {
            movement += Vector3.left * Mathf.Min(moveSpeed * Time.deltaTime, transform.position.x - minHorizontalPosition);
        }
        // Check if the mouse is near the right edge and within limits
        else if (mousePosition.x > screenWidth - edgeSize && transform.position.x < maxHorizontalPosition)
        {
            movement += Vector3.right * Mathf.Min(moveSpeed * Time.deltaTime, maxHorizontalPosition - transform.position.x);
        }

        // Check if the mouse is near the top edge and within limits
        if (mousePosition.y > screenHeight - edgeSize && transform.position.y < maxVerticalPosition)
        {
            movement += Vector3.up * Mathf.Min(moveSpeed * Time.deltaTime, maxVerticalPosition - transform.position.y);
        }
        // Check if the mouse is near the bottom edge and within limits
        else if (mousePosition.y < edgeSize && transform.position.y > minVerticalPosition)
        {
            movement += Vector3.down * Mathf.Min(moveSpeed * Time.deltaTime, transform.position.y - minVerticalPosition);
        }

        // Apply the movement
        transform.Translate(movement);
    }
}
