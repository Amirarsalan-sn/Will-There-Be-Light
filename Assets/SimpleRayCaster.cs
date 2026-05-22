using DoorScript;
using UnityEngine;

public class SimpleRaycaster : MonoBehaviour
{
    public Camera cam;
    private float maxDistance = 5f;

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(cam.transform.position,
                      cam.transform.forward * maxDistance,
                      Color.red);

        if (Physics.Raycast(cam.transform.position,
                            cam.transform.forward,
                            out hit,
                            maxDistance))
        {
            // Only interact when player presses a key, e.g. E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Try to get a Door component on what we hit
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.OnInteract();
                }
            }
        }
    }
}