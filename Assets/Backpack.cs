using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    [System.Serializable]
    public class BackpackItem
    {
        public string id;            // e.g. "flower_petal_1"
        public Sprite icon;          // image to show in UI
    }

    public GameObject backpackPanel;          // UI panel to show/hide
    public Transform itemsParent;             // parent for item UI icons
    public GameObject itemIconPrefab;         // prefab with an Image component
    public LevelManager levelManager;

    List<BackpackItem> items = new List<BackpackItem>();
    bool isOpen = false;
    bool haveDiscarded = false;

    void Start()
    {
        backpackPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleBackpack();
        }
        if (levelManager.discardItems && !haveDiscarded)
        {
            DiscardItems();
            haveDiscarded = true;
        }
    }

    public void AddItem(BackpackItem newItem)
    {
        items.Add(newItem);
        RefreshUI();
    }

    void ToggleBackpack()
    {
        isOpen = !isOpen;
        backpackPanel.SetActive(isOpen);
    }

    void DiscardItems()
    {
        items.Clear();
        RefreshUI();
    }

    void RefreshUI()
    {
        // clear previous icons
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        // create one icon per item
        foreach (var item in items)
        {
            GameObject iconGO = Instantiate(itemIconPrefab, itemsParent);
            Image img = iconGO.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = item.icon;
            }
        }
    }
}