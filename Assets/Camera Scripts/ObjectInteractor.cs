using UnityEngine;

public class ObjectInteractor : MonoBehaviour
{
    public float interactionRange = 100f; // Range of the raycast
    public LayerMask interactableLayer; // Layer of the interactable objects

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green, 2.0f);

            if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    interactable.OnInteract();
                }
            }
        }
    }
}
