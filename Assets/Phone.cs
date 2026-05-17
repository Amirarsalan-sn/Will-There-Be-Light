using TMPro;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject UIPrompt;
    public LevelManager levelManager;
    AudioSource audioSource;
    public AudioClip audioRingClip;
    public AudioClip audioAnswerClip;
    public TMP_Text UIText;
    bool playerHasAnswered = false;
    bool playerInRange = false;
    bool playerIsListenning = false;
    bool phaseChanged = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIPrompt.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioRingClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.telephoneRing && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!playerHasAnswered) {
                audioSource.Stop();
                playerHasAnswered = true;
                audioSource.clip = audioAnswerClip;
                audioSource.Play();
                playerIsListenning = true;
                UIText.text = "E (Stop)";
            } else if (playerIsListenning)
            {
                audioSource.Stop();
                playerIsListenning = false;
                UIText.text = "E (Answer)";
            } else
            {
                audioSource.Play();
                playerIsListenning = true;
                UIText.text = "E (Stop)";
            }
        }

        if (levelManager.telephoneRing && !playerInRange && playerHasAnswered)
        {
            audioSource.Stop();
            playerIsListenning = false;
            UIText.text = "E (Answer)";
        }

        if (levelManager.telephoneRing && !audioSource.isPlaying && !playerHasAnswered)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Plager enter" + "level manager enabled? " + levelManager.telephoneRing);
        if (other.CompareTag("Player") && levelManager.telephoneRing)
        {
            //Debug.Log("satisfied, setting the prompt to active");
            UIPrompt.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Plager exit" + "level manager enabled? " + levelManager.telephoneRing);
        if (other.CompareTag("Player") && levelManager.telephoneRing)
        {
            //Debug.Log("satisfied, setting the prompt to not active");

            UIPrompt.SetActive(false);
            playerInRange = false;

            if (playerHasAnswered && !phaseChanged)
            {
                levelManager.phoneAnswered();
                phaseChanged = true;
            }
        }
    }
}
