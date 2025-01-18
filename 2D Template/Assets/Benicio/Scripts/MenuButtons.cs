using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public CanvasGroup fadeInGroup;

    void Start()
    {
        if (fadeInGroup != null)
            StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        for (float a = 0; a < 1; a += Time.unscaledDeltaTime * 1.5f)
        {
            yield return null;
            fadeInGroup.alpha = a;
        }
        fadeInGroup.alpha = 1;
    }

    public void Resume()
    {
        StartCoroutine(AudioManager.PlaySound("UIButtonClick"));
        Vessel.paused = false;
        UiManager.pause.Invoke();
        Time.timeScale = 1;
    }

    public void Retry()
    {
        StartCoroutine(AudioManager.PlaySound("UIButtonClick"));
        UiManager.retry.Invoke();
    }

    public void ExitToMenu()
    {
        StartCoroutine(AudioManager.PlaySound("UIButtonClick"));
        UiManager.exit.Invoke();
    }
}
