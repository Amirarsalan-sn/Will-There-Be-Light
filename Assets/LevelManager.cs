using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private int phase = 0;
    private int level = 1;
    public Narrator narrator;
    bool runningPhase = false;
    public bool doorLevel1Allowed = false;


    IEnumerator Start()
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
    }
    void Update()
    {
        if (level == 1 && phase == 1 && !runningPhase)
        {
            runningPhase = true;
            StartCoroutine(Level1Phase1());
        }
    }

    public void flowerFound()
    {
        level = 1;
        phase = 1;
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
        doorLevel1Allowed = true;
        runningPhase = false;
    }
}