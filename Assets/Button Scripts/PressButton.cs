using UnityEngine;
using System.Collections;

public class PressButton : Interactable
{
    public GameObject shutter;  // The GameObject to move
    private float elapsedTime = 0f;
    public float duration = 1f;
    private float startY;
    private float endY = 0f;
    private bool isShut = false;

    void Start()
    {
        startY = shutter.transform.position.y;  // Capture the initial Y position of the shutter
    }

    public override void OnInteract()
    {
        base.OnInteract();
        // Add your custom interaction logic here
        OpenDoor();
    }

    void OpenDoor()
    {
        // Example logic to open the door
        Debug.Log("Shutting the door...");
        // Start the coroutine to move the shutter down
        if(!isShut)
        {
            StartCoroutine(MoveDown()); 
            isShut = true;
        }
        else
        {
            StartCoroutine(MoveUp()); 
            isShut = false;
        }
        
    }

    IEnumerator MoveDown()
    {
        while (elapsedTime < duration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new Y position
            float newY = Mathf.Lerp(startY, endY, elapsedTime / duration);

            // Apply the new position to the GameObject
            shutter.transform.position = new Vector3(shutter.transform.position.x, newY, shutter.transform.position.z);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the position is exactly at the end position after the lerp
        shutter.transform.position = new Vector3(shutter.transform.position.x, endY, shutter.transform.position.z);

        // Reset elapsedTime for potential future use
        elapsedTime = 0f;
    }

    IEnumerator MoveUp()
    {
        while (elapsedTime < duration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the new Y position
            float newY = Mathf.Lerp(endY, startY, elapsedTime / duration);

            // Apply the new position to the GameObject
            shutter.transform.position = new Vector3(shutter.transform.position.x, newY, shutter.transform.position.z);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the position is exactly at the end position after the lerp
        shutter.transform.position = new Vector3(shutter.transform.position.x, startY, shutter.transform.position.z);

        // Reset elapsedTime for potential future use
        elapsedTime = 0f;
    }
}
