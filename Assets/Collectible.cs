using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemId;
    public Sprite itemIcon;
    public GameObject promptUI;    // The "E (Collect)" text object
    LevelManager levelManager;
    bool playerInRange = false;

    private void Start()
    {
        levelManager = Object.FindAnyObjectByType<LevelManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!levelManager.collectAllowed)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!levelManager.collectAllowed)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Backpack.BackpackItem item = new Backpack.BackpackItem
            {
                id = itemId,
                icon = itemIcon
            };

            Backpack backpack = Object.FindFirstObjectByType<Backpack>();
            if (backpack != null)
            {
                backpack.AddItem(item);
            }

            if (itemId == "Gypsophila")
            {
                levelManager.flowerFound();
            }
            if (itemId == "Heart")
            {
                levelManager.heartTaken();
            }
            if (itemId == "Broken Heart")
            {
                levelManager.brokenHeartCollected();
            }
            Destroy(gameObject);
        }
    }
}