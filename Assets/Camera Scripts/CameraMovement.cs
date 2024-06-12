using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 50f; // Speed of rotation
    public float maxHorizontalRotation = 90f; // Maximum horizontal rotation (right)
    public float minHorizontalRotation = -90f; // Minimum horizontal rotation (left)
    public float adjustedRotation = 0f;

    public float edgeSize = 50f; // Size of the edge area
    public GameObject uiRightButton;
    private bool uiRightButtonActivated;

    private bool screenLimit;
    private bool rightSide;

    void Start()
    {
        originalRotation = transform.rotation;
        screenLimit = true;
        rightSide = false;
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
        if (screenLimit)
        {
            float currentRotationY = transform.eulerAngles.y;
            if (currentRotationY > 180f)
            {
                currentRotationY -= 360f;
            }
            currentRotationY = Mathf.Clamp(currentRotationY, minHorizontalRotation, maxHorizontalRotation);
            transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

            // Right Button
            // Check if the camera is at the maximum right rotation
            if (currentRotationY >= maxHorizontalRotation && !rightSide)
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
    }

    public void RightTurn()
    {
        screenLimit = false;
        StartCoroutine(SmoothRotate(new Vector3(0f, 110f, 0f), .25f)); // Smoothly rotate by 90 degrees in 1 second
        minHorizontalRotation = 90 + minHorizontalRotation;
        maxHorizontalRotation = 90 + maxHorizontalRotation;
        screenLimit = true;
        rightSide = true;
    }

    IEnumerator SmoothRotate(Vector3 targetEulerAngles, float duration)
    {
        Quaternion originalRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the rotation finishes exactly at the target
        transform.rotation = targetRotation;
    }
}
