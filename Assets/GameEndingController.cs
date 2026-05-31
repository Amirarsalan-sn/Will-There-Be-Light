using UnityEngine;
using System.Collections;

public class GameEndingController : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;  // on GameEndingPanel
    public CanvasGroup title;
    public CanvasGroup body1;
    public CanvasGroup body2;
    public CanvasGroup body3;
    public CanvasGroup body4;
    public CanvasGroup body5;
    public CanvasGroup body6;
    public CanvasGroup body7;
    public CanvasGroup body8;
    public CanvasGroup body9;
    public CanvasGroup body10;
    public CanvasGroup body11;
    public CanvasGroup body12;
    public CanvasGroup body13;

    private float firstFadeDuration = 5f;
    private float afterFadeDuration = 2f;
    private float holdDuration = 2f;

    public void StartGameEnding()
    {
        StartCoroutine(GameEndingSequence());
    }

    IEnumerator GameEndingSequence()
    {
        /*// 1) Fade in
        yield return StartCoroutine(FadeInPanel());
        // 2) Scroll credits
        yield return StartCoroutine(ScrollBody());*/
        yield return StartCoroutine(PanelFadeIn(panelCanvasGroup, firstFadeDuration/2));


        yield return StartCoroutine(FadePanel(title, firstFadeDuration/2, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body1, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body2, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body3, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body4, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body5, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body6, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body7, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body8, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body9, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body10, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body11, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(FadePanel(body12, afterFadeDuration, afterFadeDuration));
        yield return StartCoroutine(PanelFadeIn(body13, afterFadeDuration)); 

        
        // 3) Wait for any key and quit
        yield return StartCoroutine(WaitForAnyKey());

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /*IEnumerator FadeInPanel()
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
    }*/

    IEnumerator FadePanel(CanvasGroup panel, float fadeInDuration, float fadeOutDuration)
    {
        float startAlpha = 0f;
        float endAlpha = 1f;

        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeInDuration);

            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, alpha);
            yield return null;
        }

        panel.alpha = endAlpha;

        yield return new WaitForSeconds(holdDuration);

        startAlpha = 1f;
        endAlpha = 0f;

        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeInDuration);

            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, alpha);
            yield return null;
        }
        panel.alpha = endAlpha;
    }

    IEnumerator PanelFadeIn(CanvasGroup panel, float fadeInDuration) {
        float startAlpha = 0f;
        float endAlpha = 1f;

        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeInDuration);

            panel.alpha = Mathf.Lerp(startAlpha, endAlpha, alpha);
            yield return null;
        }

        panel.alpha = endAlpha;
    }

    /*IEnumerator ScrollBody()
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
    }*/

    IEnumerator WaitForAnyKey()
    {
        yield return null; // avoid using a key that was already down

        while (!Input.anyKeyDown)
        {
            yield return null;
        }
    }
}