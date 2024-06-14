using UnityEngine;

public class PressButton: Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();
        // Add your custom interaction logic here
        OpenDoor();
    }

    void OpenDoor()
    {
        // Example logic to open the door
        Debug.Log("Opening the door...");
    }
}
