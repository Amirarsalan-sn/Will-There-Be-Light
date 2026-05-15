using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Narrator : MonoBehaviour
{
    public GameObject panel;          // NarratorPanel
    public TMP_Text narratorText;     // main text
    public Button continueButton;     // Continue button
    public TMP_Text continueButtonLabel;

    public float typeSpeed = 0.03f;   // seconds per character

    bool waitingForInput = false;

    void Start()
    {
        panel.SetActive(false);
        if (continueButton != null)
            continueButton.gameObject.SetActive(false);
    }

    public IEnumerator ShowMessage(string message)
    {
        panel.SetActive(true);
        narratorText.text = "";

        // Disable button while typing
        continueButton.gameObject.SetActive(false);
        continueButton.onClick.RemoveAllListeners();

        // Typewriter effect
        foreach (char c in message)
        {
            narratorText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // Now enable button and wait for click OR Enter key
        waitingForInput = true;
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.AddListener(OnContinueClicked);

        while (waitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnContinueClicked();
            }
            yield return null;
        }

        panel.SetActive(false);
    }
    public void SetButtonText(string text)
    {
        continueButtonLabel.text = text;
    }
    void OnContinueClicked()
    {
        waitingForInput = false;
    }
}