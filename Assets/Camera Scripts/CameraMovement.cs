using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 50f; // Speed of rotation
    public float maxHorizontalRotation = 90f; // Maximum horizontal rotation (right)
    public float minHorizontalRotation = -90f; // Minimum horizontal rotation (left)

    public float edgeSize = 50f; // Size of the edge area
    public GameObject uiRightButton;
    private bool uiRightButtonActivated;

    void Start()
    {
        // ButtonRight buttonRight = instantiatedButton.GetComponentInChildren<ButtonRight>();
        originalRotation = transform.rotation;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;

        float rotationAmount = 0f;

        // Check if the cursor is near the left edge of the screen
        if (mousePosition.x < edgeSize)
        {
            rotationAmount = -rotationSpeed * Time.deltaTime;
        }
        // Check if the cursor is near the right edge of the screen
        else if (mousePosition.x > screenWidth - edgeSize)
        {
            rotationAmount = rotationSpeed * Time.deltaTime;
        }

        // Apply the rotation
        transform.Rotate(Vector3.up, rotationAmount);

        // Ensure rotation stays within limits
        float currentRotationY = transform.eulerAngles.y;
        if (currentRotationY > 180f)
        {
            currentRotationY -= 360f;
        }
        currentRotationY = Mathf.Clamp(currentRotationY, minHorizontalRotation, maxHorizontalRotation);
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

        // Right Button
        // Check if the camera is at the maximum right rotation
        if (currentRotationY >= maxHorizontalRotation)
        {
        // Get the RectTransform of the button
        RectTransform rectTransform = uiRightButton.GetComponent<RectTransform>();

        // Set the position of the RectTransform
        rectTransform.anchoredPosition = new Vector2(0f, 0f); // Set the position to (0, 0) when camera is at max horizontal rotation
        }
        // If the camera moves away from the maximum right rotation and the button is instantiated, destroy the button
        else
        {
        // Get the RectTransform of the button
        RectTransform rectTransform = uiRightButton.GetComponent<RectTransform>();

        // Set the position of the RectTransform
        rectTransform.anchoredPosition = new Vector2(100f, 0f); // Set the position to (100, 0) when camera is not at max horizontal rotation
        }
    }

    public void ResetRotation()
    {
        transform.rotation = originalRotation;
    }

    public void TurnRight()
    {
        Debug.Log('w');
    }
}