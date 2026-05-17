using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    private int phase = 2; // remember to change it
    private int level = 1;
    public Narrator narrator;
    bool runningPhase = false;
    public bool doorAllowed = false;
    public bool telephoneRing = false;
    public bool gramaphoneAllowed = false;
    public GameObject loveLetter;
    public GameObject myLetter;
    public GameObject heart;

    private void Start()
    {
        loveLetter.SetActive(false);
        myLetter.SetActive(false);
        heart.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Update()
    {
        switch (level)
        {
            case 1:
                switch (phase)
                {
                    case 0:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level1Phase0());
                        }
                        break;
                    case 1:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level1Phase1());
                        }
                        break;
                    case 2:
                        doorAllowed = true;
                        break;
                }
                break;
            case 2:
                switch (phase)
                {
                    case 1:
                        doorAllowed = false;
                        telephoneRing = true;
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase1());
                        }
                        break;
                    case 2: // showing the love letter
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase2());
                        }
                        break;
                    case 3:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase3());
                        }
                        break;
                    case 4:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase4());
                        }
                        break;
                    case 5:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase5());
                        }
                        break;
                    case 6:
                        doorAllowed = true;
                        break;
                    case 7:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level2Phase7());
                        }
                        break;

                }
                break;
        }
    }

    public void flowerFound()
    {
        level = 1;
        phase = 1;
    }

    public void heartTaken()
    {
        level = 2;
        phase = 5;
        runningPhase = false;
    }

    public void allowGramaphone()
    {
        gramaphoneAllowed = true;
    }
    public void loveLetterPicked()
    {
        if (level == 2 && phase == 2)
        {
            level = 2;
            phase = 3;
            runningPhase = false;
            myLetter.SetActive(true);
        } else if (level == 2 && phase ==3)
        {
            level = 2;
            phase = 4;
            runningPhase = false;
            heart.SetActive(true);
        }
    }

    public void phoneAnswered()
    {
        level = 2;
        phase = 2;
        loveLetter.SetActive(true); // show loveLetter.
        runningPhase = false;
    }

    public void level2EntryPassed()
    {
        level = 2;
        phase = 1;
    }

    public void level2CorridorReached()
    {
        level = 2;
        phase = 7;
        gramaphoneAllowed = false;
        doorAllowed = false;
    }

    public void level3EntryReached()
    {
        level = 2;
        phase = 7;
        doorAllowed = true;
    }

    public void level3Entered()
    {
        level = 3;
        phase = 1;
        runningPhase = false;
    }
    IEnumerator Level1Phase0()
    {
        narrator.SetButtonText("Continue");
        yield return new WaitForSeconds(3);
        // Example: introductory line
        yield return StartCoroutine(narrator.ShowMessage(
            @"Welcome... to my brain :))
I have lost my flower, could you find it for me please?"
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            @"Thank you very much!! It's a white Gypsophila, it's somewhere around here, please bring it back to me as soon as possible."
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            @"What ?! You don't know what is a white Gypsophila? It's fine don't worry, my flower is very special! You will for sure recognize it once you see it :)"
        ));
        // Then enable movement or start gameplay, etc.
        level = 1;
        phase = -1;
        runningPhase = false;
    }

    IEnumerator Level1Phase1()
    {
        yield return StartCoroutine(narrator.ShowMessage(
            "OMG you found it :'). Thank you, Thank you, Thank you so much!."
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "Hmmm, this reminds me of something, wonder what it is..."
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "Oh!! Looks like you got yourself a ticket to a precious part of my memory. " +
            "Head over to the door next to the bed, once you reached it press E to open it."
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "By the way! You have a backpack with you in which the collected items are stored. You can open your backpack and see the collected items by pressing P."
        ));

        // After sequence is done, advance state
        level = 1;
        phase = 2;
        runningPhase = false;
    }

    IEnumerator Level2Phase1()
    {
        yield return StartCoroutine(narrator.ShowMessage(
            "Looks like someone's calling. Don't you wanna answer the phone?"
        ));
    }

    IEnumerator Level2Phase2() {
        yield return StartCoroutine(narrator.ShowMessage(
            "Looks like you have a letter, it's on your desk. I'm curious to find out what does it say !!!"
        ));

    }
    IEnumerator Level2Phase3() {
        yield return StartCoroutine(narrator.ShowMessage(
            "Wow ... :), NICE!\nDon't you wanna reply the letter?"
        ));
    }

    IEnumerator Level2Phase4() {
        yield return StartCoroutine(narrator.ShowMessage(
            ":)...\nGo on...\nTake the big heart...\nIt's for you :\")"
        ));
    }

    IEnumerator Level2Phase5()
    {
        yield return StartCoroutine(narrator.ShowMessage(
            "Woow your back pack is almost full!\nSo glad you found someone in your life body! Wish the best of bests for you two. It must be an awsome feeling, and extraordinary experience, something you cannot compare to anything else!"
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "Let's see what destiny has chosen for us in the future, I'm so excited to see.\nLets go to the next room to find out."
        ));

        level = 2;
        phase = 6;
        runningPhase = false;
    }

    IEnumerator Level2Phase7() {
        yield return StartCoroutine(narrator.ShowMessage(
            "What was that sound!"
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "The door was slammed shut!!"
        ));

        yield return StartCoroutine(narrator.ShowMessage(
            "Hmm, nevermind, hopefully nothing bad has happened, let's move forward."
        ));
    }
}