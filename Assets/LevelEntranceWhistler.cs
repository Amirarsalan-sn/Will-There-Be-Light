using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEntranceWhistler : MonoBehaviour
{
    public int level;
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
                case 2:
                    levelManager.level2EntryPassed();
                    break;
            }
        }
    }
}
