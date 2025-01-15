using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UnityEvent pause = new();
    public static UnityEvent retry = new();

    public Image tint;
    public Image black;
    
    float fadeTarget;

    void Start()
    {
        pause.AddListener(Pause);
        retry.AddListener(Retry);
        fadeTarget = tint.color.a;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        for (float a = 1; a < 0; a -= Time.unscaledDeltaTime)
        {
            yield return null;
            black.color = new(0, 0, 0, a);
        }
        black.color = new(0, 0, 0, 0);
    }

    void Pause()
    {
        if (Vessel.paused)
        {
            StartCoroutine(FadeTint(0.75f));
        }
        else
        {
            StartCoroutine(FadeTint(0));
        }
    }

    void Retry()
    {
        StartCoroutine(RetrySequence());
    }
    IEnumerator RetrySequence()
    {
        for (float a = 0; a < 1; a += Time.unscaledDeltaTime)
        {
            yield return null;
            black.color = new(0, 0, 0, a);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FadeTint(float target)
    {
        if (tint.color.a == fadeTarget)
        {
            fadeTarget = target;
            float fadeDirection = Mathf.Sign(fadeTarget - tint.color.a);
            while (fadeTarget * fadeDirection > tint.color.a * fadeDirection)
            {
                tint.color = new(0, 0, 0, tint.color.a + fadeDirection * Time.unscaledDeltaTime);
                yield return null;
                fadeDirection = Mathf.Sign(fadeTarget - tint.color.a);
            }
            tint.color = new(0, 0, 0, fadeTarget);
        }
        else
            fadeTarget = target;
    }
}
