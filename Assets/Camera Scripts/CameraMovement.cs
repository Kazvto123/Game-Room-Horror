using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 50f; // Speed of rotation
    public float maxHorizontalRotation = 90f; // Maximum horizontal rotation (right)
    public float minHorizontalRotation = -90f; // Minimum horizontal rotation (left)
    
    public float edgeSize = 50f; // Size of the edge area
    public GameObject uiRightButton;
    public GameObject uiLeftButton;

    private bool screenLimit;
    private bool canRotating;

    void Start()
    {
        originalRotation = transform.rotation;
        screenLimit = true;
        canRotating = true;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float rotationAmount = 0f;

        if (canRotating)
        {
            if (mousePosition.x < edgeSize)
            {
                rotationAmount = -rotationSpeed * Time.deltaTime;
            }
            else if (mousePosition.x > screenWidth - edgeSize)
            {
                rotationAmount = rotationSpeed * Time.deltaTime;
            }
            
            transform.Rotate(Vector3.up, rotationAmount);
        }

        if (screenLimit)
        {
            float currentRotationY = transform.eulerAngles.y;
            if (currentRotationY > 180f)
            {
                currentRotationY -= 360f;
            }
            currentRotationY = Mathf.Clamp(currentRotationY, minHorizontalRotation, maxHorizontalRotation);
            transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

            UpdateUIButtonPosition(currentRotationY);
        }
    }

    private void UpdateUIButtonPosition(float currentRotationY)
    {
        RectTransform rightButtonRectTransform = uiRightButton.GetComponent<RectTransform>();
        RectTransform leftButtonRectTransform = uiLeftButton.GetComponent<RectTransform>();

        if (currentRotationY >= maxHorizontalRotation)
        {
            rightButtonRectTransform.anchoredPosition = new Vector2(0f, 0f); // Show right button
        }
        else
        {
            rightButtonRectTransform.anchoredPosition = new Vector2(100f, 0f); // Hide right button
        }

        if (currentRotationY <= minHorizontalRotation)
        {
            leftButtonRectTransform.anchoredPosition = new Vector2(0f, 0f); // Show left button
        }
        else
        {
            leftButtonRectTransform.anchoredPosition = new Vector2(-100f, 0f); // Hide left button
        }
    }

    public void RightTurn()
    {
        canRotating = false;
        screenLimit = false;
        StartCoroutine(SmoothRotate(Vector3.up * 90f, 0.25f, true)); // Smoothly rotate by 90 degrees in 0.25 seconds
    }

    public void LeftTurn()
    {
        canRotating = false;
        screenLimit = false;
        StartCoroutine(SmoothRotate(Vector3.up * -90f, 0.25f, false)); // Smoothly rotate by -90 degrees in 0.25 seconds
    }

    IEnumerator SmoothRotate(Vector3 rotationAmount, float duration, bool isRightTurn)
    {
        Quaternion originalRotation = transform.rotation;
        Quaternion targetRotation = originalRotation * Quaternion.Euler(rotationAmount);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        
        if (isRightTurn)
        {
            minHorizontalRotation += 90f;
            maxHorizontalRotation += 90f;
        }
        else
        {
            minHorizontalRotation -= 90f;
            maxHorizontalRotation -= 90f;
        }

        screenLimit = true;
        canRotating = true;
    }
}
