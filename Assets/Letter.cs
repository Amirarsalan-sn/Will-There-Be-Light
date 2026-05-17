using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Letter : MonoBehaviour
{
    public string itemId;
    public GameObject UIPrompt;
    public LevelManager levelManager;
    public GameObject panel;
    public Button button;
    public Sprite itemIcon = null;
    bool playerInRange = false;
    bool isCollectible = false;
    bool letterOpenned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        UIPrompt.SetActive(false);
        panel.SetActive(false);
        if (this.CompareTag("LoveLetter"))
        {
            isCollectible = true;
        }
    }

    void Start()
    {
        UIPrompt.SetActive(false);
        panel.SetActive(false);
        if (this.CompareTag("LoveLetter"))
        {
            isCollectible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !letterOpenned)
        {
            levelManager.allowGramaphone();
            panel.SetActive(true);
            button.onClick.AddListener(OnClicked);
            letterOpenned = true;
        } else if (letterOpenned)
        {

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClicked();
                panel.SetActive(false);
                UIPrompt.SetActive(false);
                Destroy(gameObject);
            }         
        }
    }

    private void OnClicked()
    {
        if (isCollectible)
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
        }
        levelManager.loveLetterPicked();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPrompt.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIPrompt.SetActive(false);
            playerInRange = false;
        }
    }
}
