using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string itemId;
    public Sprite itemIcon;
    public GameObject promptUI;    // The "E (Collect)" text object

    bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
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
                Object.FindAnyObjectByType<LevelManager>().flowerFound();
            }
            Destroy(gameObject);
        }
    }
}