using UnityEngine;
using System.Collections;
using TMPro;

public class ButtonPress : MonoBehaviour
{
    public float pressDepth = 0.05f;     // how far down the button moves
    public float pressTime = 0.1f;       // time to go down
    public float releaseTime = 0.1f;     // time to go up
    AudioSource clickSource;      // assigned in Inspector
    public AudioClip ac;
    public GameObject text;
    public LevelManager levelManager;
    int pressCount = 0;
    bool announced20 = false;
    bool announced40 = false;
    bool announced60 = false;
    bool announced80 = false;
    bool announced100 = false;
    bool announced120 = false;

    Vector3 initialLocalPosition;
    bool isAnimating = false;
    bool playerInRange = false;

    void Start()
    {
        initialLocalPosition = transform.localPosition;
        clickSource = GetComponent<AudioSource>();
        clickSource.clip = ac;
        text.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isAnimating && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PressRoutine());
        }

        if (pressCount == 20 && !announced20)
        {
            announced20 = true;
            levelManager.pushPassed20();
        } else if (pressCount == 40 && !announced40)
        {
            announced40 = true;
            levelManager.pushPassed40();
        } else if (pressCount == 60 && !announced60)
        {
            announced60 = true;
            levelManager.pushPassed60();
        } else if (pressCount == 80 && !announced80)
        {
            announced80 = true;
            levelManager.pushPassed80();
        } else if (pressCount == 100 && !announced100)
        {
            announced100 = true;
            levelManager.pushPassed100();
        } else if (pressCount == 120 && !announced120)
        {
            announced120 = true;
            levelManager.pushPassed150();
        }
    }

    IEnumerator PressRoutine()
    {
        isAnimating = true;
        pressCount++;

        if (clickSource != null)
            clickSource.Play();

        // Move down
        Vector3 targetDown = initialLocalPosition + new Vector3(0f, -pressDepth, 0f);
        float t = 0f;
        while (t < pressTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / pressTime);
            transform.localPosition = Vector3.Lerp(initialLocalPosition, targetDown, alpha);
            yield return null;
        }

        // Move back up
        t = 0f;
        while (t < releaseTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / releaseTime);
            transform.localPosition = Vector3.Lerp(targetDown, initialLocalPosition, alpha);
            yield return null;
        }

        transform.localPosition = initialLocalPosition;
        isAnimating = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Here you can enable your "E (Push)" text if you want.
            text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Disable "E (Push)" text here.
            text.SetActive(false);
        }
    }
}