using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BloomController : MonoBehaviour
{
    public Volume volume;        // assign your Global Volume in Inspector

    Bloom bloom;

    void Start()
    {
        if (volume.profile.TryGet(out bloom))
        {
            bloom.intensity.overrideState = true;
        }
    }

    public void IncreaseBloomOverTime(float targetIntensity, float duration)
    {
        if (bloom != null)
        {
            StartCoroutine(BloomRoutine(targetIntensity, duration));
        }
    }

    System.Collections.IEnumerator BloomRoutine(float targetIntensity, float duration)
    {
        float startIntensity = bloom.intensity.value;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / duration);

            // Lerp from startIntensity to targetIntensity
            bloom.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, alpha);

            yield return null;
        }

        // Snap exactly to target at the end
        bloom.intensity.value = targetIntensity;
    }
}