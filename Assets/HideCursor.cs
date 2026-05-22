using UnityEngine;

public class HideCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;                          // hide cursor
        Cursor.lockState = CursorLockMode.Locked;        // lock to center (optional but common)
    }
}