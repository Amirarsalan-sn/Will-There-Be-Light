using UnityEngine;
using System.Collections;

public class GameEndingController : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;  // on GameEndingPanel
    public RectTransform body;           // same as before

    private float fadeDuration = 5f;
    private float bodyTargetY = 4100f;
    private float scrollDuration = 10f;

    public void StartGameEnding()
    {
        StartCoroutine(GameEndingSequence());
    }

    IEnumerator GameEndingSequence()
    {
        // 1) Fade in
        yield return StartCoroutine(FadeInPanel());
        // 2) Scroll credits
        yield return StartCoroutine(ScrollBody());

        // 3) Wait for any key and quit
        yield return StartCoroutine(WaitForAnyKey());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator FadeInPanel()
    {
        float startAlpha = 0f;
        float endAlpha = 1f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);

            panelCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, alpha);
            yield return null;
        }

        panelCanvasGroup.alpha = endAlpha;
    }

    IEnumerator ScrollBody()
    {
        Vector2 startPos = body.anchoredPosition;
        Vector2 targetPos = new Vector2(startPos.x, bodyTargetY);
        Debug.Log(startPos + " " + targetPos);

        float t = 0f;
        while (t < scrollDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / scrollDuration);
            body.anchoredPosition = Vector2.Lerp(startPos, targetPos, alpha);
            Debug.Log(body.anchoredPosition + " " + targetPos);
            yield return null;
        }

        body.anchoredPosition = targetPos;
    }

    IEnumerator WaitForAnyKey()
    {
        yield return null; // avoid using a key that was already down

        while (!Input.anyKeyDown)
        {
            yield return null;
        }
    }
}