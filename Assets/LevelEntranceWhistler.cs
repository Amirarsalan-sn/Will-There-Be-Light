using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEntranceWhistler : MonoBehaviour
{
    public float level;
    public LevelManager levelManager;
    bool hasPassedBefore = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (hasPassedBefore)
        {
            return;
        }
        else
        {
            hasPassedBefore = true;
        }

        if (other.CompareTag("Player"))
        {
            switch (level)
            {
                case 2f:
                    levelManager.level2EntryPassed();
                    break;
                case 2.5f:
                    levelManager.level2CorridorReached(); 
                    break;
                case 2.8f:
                    levelManager.level3EntryReached();
                    break;
            }
        }
    }
}
