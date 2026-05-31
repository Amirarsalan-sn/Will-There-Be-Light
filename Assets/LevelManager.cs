using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public AudioSource backGroundSource;
    public AudioClip backGroundClip;
    public AudioSource heartBeatSource;
    public AudioClip heartBeatCalmClip;
    public AudioClip heartBeatStressClip;
    public AudioSource phoneRingChaos1Source;
    public AudioClip phoneRingChaos1Clip;
    public AudioSource phoneRingChaos2Source;
    public AudioClip phoneRingChaos2Clip;
    public AudioSource churchBellSource;
    public AudioClip churchBellClip;
    public AudioSource fireAlarmSource;
    public AudioClip fireAlarmClip;
    public AudioSource fireAlarmNarrativeSource;
    public AudioClip fireAlarmNarrativeClip;
    public AudioClip outroMusic;
    public AudioSource wallMoveSource;
    public AudioClip wallMoveClip;
    public BloomController bloomController;
    private int phase = 0; // remember to change it
    private int level = 1;
    public Narrator narrator;
    bool runningPhase = false;
    public bool doorAllowed = false;
    public bool telephoneRing = false;
    public bool gramaphoneAllowed = false;
    public bool discardItems = false;
    public bool collectAllowed = false;
    public bool answerAllowed = false;
    public GameObject loveLetter;
    public GameObject myLetter;
    public GameObject heart;

    // level 3 prefabs.
    public GameObject fixButton;
    public GameObject furniture1;
    public GameObject furniture2;
    public GameObject furniture3;
    public GameObject textGroup1;
    public GameObject textGroup2;
    public GameObject textGroup3;
    public GameObject textGroup4;
    public GameObject textGroup5;
    public Light topLeftCorner1;
    public Light topLeftCorner2;
    public Light topRightCorner;
    public Light bottomRightCorner;
    public GameObject corridorEntered;
    public GameObject level2whistler1;
    public GameObject level2whistler2;
    public GameObject level3OutroWhistler;
    public GameObject brokenHeart;
    public GameObject wall;
    Vector3 initialWallPos;
    float wallDropAmount = 4f;
    float wallDropTime = 10f;
    public bool wallDown = false;
    public GameEndingController endingController;

    private void Start()
    {
        loveLetter.SetActive(false);
        myLetter.SetActive(false);
        heart.SetActive(false);
        backGroundSource.clip = backGroundClip;
        heartBeatSource.clip = heartBeatCalmClip;
        phoneRingChaos1Source.clip = phoneRingChaos1Clip;
        phoneRingChaos2Source.clip = phoneRingChaos2Clip;
        churchBellSource.clip = churchBellClip;
        fireAlarmSource.clip = fireAlarmClip;
        fireAlarmNarrativeSource.clip = fireAlarmNarrativeClip;
        wallMoveSource.clip = wallMoveClip;
        backGroundSource.Play();

        // level3 activations

        textGroup1.SetActive(false);
        textGroup2.SetActive(false);
        textGroup3.SetActive(false);
        textGroup4.SetActive(false);
        textGroup5.SetActive(false);
        fixButton.SetActive(false);
        corridorEntered.SetActive(false);
        brokenHeart.SetActive(false);

        initialWallPos = wall.transform.position;
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
                            collectAllowed = false;
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
                        collectAllowed = false;
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
            case 3:
                switch (phase)
                {
                    case 1:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level3Phase1());
                        }
                        break;

                    case 2:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            fixButton.SetActive(true);
                            heartBeatSource.Stop();
                            heartBeatSource.clip = heartBeatStressClip;
                            heartBeatSource.Play();
                            churchBellSource.Play();
                        }
                        break;

                    case 3:

                        if(!runningPhase)
                        {
                            runningPhase = true;
                            phoneRingChaos1Source.Play();
                            textGroup1.SetActive(true);
                            furniture1.SetActive(false);
                            StartCoroutine(Level3Phase2());
                        }
                        break;
                    case 4: //40

                        if(!runningPhase)
                        {
                            runningPhase= true;
                            phoneRingChaos2Source.Play();
                            textGroup2.SetActive(true);
                            furniture2.SetActive(false);
                        }
                        break;
                    case 5: //60
                        if (!runningPhase)
                        {
                            runningPhase= true;
                            fireAlarmSource.Play();
                            textGroup3.SetActive(true);
                            furniture3.SetActive(false);
                            StartCoroutine(Level3Phase5());
                        }
                        break;
                    case 6: //80
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            textGroup4.SetActive(true);
                        }
                        break;
                    case 7: // 100
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            textGroup5.SetActive(true);
                            fireAlarmNarrativeSource.Play();
                            phoneRingChaos1Source.Stop();
                            phoneRingChaos2Source.Stop();
                            topLeftCorner1.color = Color.red;
                            topLeftCorner2.color = Color.red;
                            topRightCorner.color = Color.red;
                            bottomRightCorner.color = Color.red;
                        }
                        break;
                    case 8:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level3Phase8());
                            doorAllowed = true;
                            corridorEntered.SetActive(true);
                            level2whistler1.SetActive(false);
                            level2whistler2.SetActive(false);
                        }
                        break;
                    case 9:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            corridorEntered.SetActive(false);
                            churchBellSource.Stop();
                            fireAlarmNarrativeSource.Stop();
                            fireAlarmSource.Stop();
                            doorAllowed = false;
                            StartCoroutine(Level3Phase9());
                        }
                        break;
                    case 10:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            heartBeatSource.clip = outroMusic;
                            collectAllowed = true;
                            heartBeatSource.Play();
                            brokenHeart.SetActive(true);
                        }
                        break;
                    case 11:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            StartCoroutine(Level3Phase11());
                            bloomController.IncreaseBloomOverTime(10, 10);
                        }
                        break;
                    case 12:
                        if(!runningPhase)
                        {
                            level3OutroWhistler.SetActive(false);
                            runningPhase = true;
                            StartCoroutine(Level3Phase12());
                        }
                        break;
                    case 13:
                        if (!runningPhase)
                        {
                            runningPhase = true;
                            endingController.StartGameEnding();
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

    public void pushPassed20()
    {
        level = 3;
        phase = 3;
        runningPhase = false;
    }

    public void pushPassed40()
    {
        level = 3;
        phase = 4;
        runningPhase = false;
    }

    public void pushPassed60()
    {
        level = 3;
        phase = 5;
        runningPhase = false;
    }

    public void pushPassed80()
    {
        level = 3;
        phase = 6;
        runningPhase = false;
    }
    public void pushPassed100()
    {
        level = 3;
        phase = 7;
        runningPhase = false;
    }

    public void pushPassed150()
    {
        level = 3;
        phase = 8;
        runningPhase = false;
    }

    public void brokenHeartCollected()
    {
        level = 3;
        phase = 11;
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
        doorAllowed = false;
        runningPhase = false;
    }

    public void level3CorridorReached()
    {
        level = 3;
        phase = 9;
        runningPhase = false;
    }

    public void level3FinalHallReached()
    {
        level = 3;
        phase = 12;
        runningPhase = false;
    }

    IEnumerator Level1Phase0()
    {
        narrator.SetButtonText("(Enter)");
        yield return new WaitForSeconds(3);
        // Example: introductory line

        yield return StartCoroutine(narrator.ShowMessage(
            @"Hi :))
Before we start, I would like to give you a tutorial:
You can move with W/S/A/D + mouse.
In order to collect an object (collectable ones) you just need to get close to it and press E.
In order to see if a door is locked or not you should get close to it, once you were close enough, just look at it and press E.
In order to open an unlocked door, get closer to it and press E.
The doors close automatically (or manually by again pressing E).
"
        ));
        narrator.SetButtonText("Continue");
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
        collectAllowed = true;
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
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(narrator.ShowMessage(
            "Looks like someone's calling. Don't you wanna answer the phone?"
        ));
        answerAllowed = true;
    }

    IEnumerator Level2Phase2() {
        yield return new WaitForSeconds(3);
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
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(narrator.ShowMessage(
            ":)...\nGo on...\nTake the big heart...\nIt's for you :\")"
        ));

        collectAllowed = true;
    }

    IEnumerator Level2Phase5()
    {
        yield return StartCoroutine(narrator.ShowMessage(
            "Woow your backpack is almost full!\nSo glad you found someone in your life buddy! Wish the best of bests for you two. It must be an awsome feeling, and extraordinary experience, something you cannot compare to anything else!"
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
        heartBeatSource.clip = heartBeatCalmClip;
        heartBeatSource.Play();
    }

    IEnumerator Level3Phase1() {
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(narrator.ShowMessage(
            "...\nLooks like you have ran into conflicts...\nBut hey, there's nothing to worry about, things happen... people discuss... and then they solve their problems.\nYou guys can of course work it out!"
        ));

        level = 3;
        phase = 2;
        runningPhase = false;
    }

    IEnumerator Level3Phase2()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(narrator.ShowMessage(
            "Don't let anything distract you, KEEP PUSHING!"
        ));
    }

    IEnumerator Level3Phase5() {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(narrator.ShowMessage(
            "KEEP PUSHINGGG!!!"
        ));
    }

    IEnumerator Level3Phase8() {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(narrator.ShowMessage(
            "STOP... STOP IT...\nYOU DID EVERYTHING YOU COULD, NOTHING IS REMAINED OF YOU, GET OUT OF THE ROOM\nNOW!"
        ));
    }

    IEnumerator Level3Phase9() {
        yield return new WaitForSeconds(3);

        narrator.SetButtonText("What?!");
        yield return StartCoroutine(narrator.ShowMessage(
            "Wait...\nWhy did you do that?"
        ));

        narrator.SetButtonText("Yes :(");
        yield return StartCoroutine(narrator.ShowMessage(
            "Why did you turn back? You know you can't reverse time right?"
        ));

        yield return new WaitForSeconds(20);


        narrator.SetButtonText("No :(");
        yield return StartCoroutine(narrator.ShowMessage(
            "My friend\nThere's no way to the previous room. It's over...\nYou did what you could\nYou should let go..."
        ));

        yield return new WaitForSeconds(5);

        narrator.SetButtonText("No!");
        yield return StartCoroutine(narrator.ShowMessage(
            "I know...\nI know how you feel\nI feel the pain in your heart\nCome on buddy... there's nothing you can do... you did EVERYTHING you could\nJust... let it go..."
        ));

        //yield return new WaitForSeconds(5);
        narrator.SetButtonText("NO!!!");
        yield return StartCoroutine(narrator.ShowMessage(
            "The belongings in your backpack...\nIt's a heavy burden upon your shoulders...\nYou MUST let go..."
        ));

        //yield return new WaitForSeconds(5);
        narrator.continueButtonLabel.fontSize = 18;
        narrator.SetButtonText("Empty backpack");
        yield return StartCoroutine(narrator.ShowMessage(
            "I know how much you have tried...\nHow much you have faught...You don't have to fight your own battles anymore...\nThat's life... things happen... hey that's not the end of the world!!\nCome on buddy, I know you can do it, I know you can..."
        ));
        heartBeatSource.Stop();
        yield return new WaitForSeconds(3);

        discardItems = true;
        level = 3;
        phase = 10;
        runningPhase = false;
    }

    IEnumerator Level3Phase11()
    {
        Transform wallTransform = wall.transform;
        wallMoveSource.Play();
        Vector3 start = initialWallPos;
        Vector3 target = initialWallPos + Vector3.down * wallDropAmount;
        float t = 0f;
        while (t < wallDropTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / wallDropTime);
            wallTransform.position = Vector3.Lerp(start, target, alpha);
            yield return null;
        }

        wallTransform.position = target;
        wallMoveSource.Stop();
        wallDown = true;
        yield return null;
    }

    IEnumerator Level3Phase12() {
        yield return new WaitForSeconds(3);

        narrator.continueButtonLabel.fontSize = 24;
        narrator.SetButtonText("Continue");
        yield return StartCoroutine(narrator.ShowMessage(
            "You...\nYou did it!!!!"
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "I KNEW IT...\nI KNEW YOU CAN DO IT."
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "Of course it still hurts, it's supposed to...\nIt's gonna be like this for a long time unfortunately :("
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "\"That's life\"...\nThat is what most people say, and honestly, I HATE IT.\nI hate when life acts like this and you just have to accept it because \"That's life\"??! Nonesense !\nBut... what can I say... after all it's true :("
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "Be proud of yourself, you did what requires lots of bravery, what I couldn't do... \nDon't worry laddie... it's hard... but it'll pass...\nYou're gonna heal... I believe in you!"
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "Well, that was my story...\nI know... I know...\nLife is still full of joy and wonders, full of undiscovered things...\nBut still, it sucks sometimes...\nSometimes it leaves a scare that hardly heals.\nI try to be grateful... but... my question to you is..."
        ));

        yield return new WaitForSeconds(5);
        yield return StartCoroutine(narrator.ShowMessage(
            "Will there be light for me in real life ??? :) ..."
        ));
        yield return new WaitForSeconds(5);
        level = 3;
        phase = 13;
        runningPhase = false;
    }
}