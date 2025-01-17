using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UnityEvent pause = new();
    public static UnityEvent retry = new();
    public static UnityEvent exit = new();
    public static UnityEvent fail = new();
    public static string failDetails;

    public Image tint;
    public Image black;
    public GameObject pauseMenu;
    public GameObject failMenu;
    
    float fadeTarget;

    void Start()
    {
        failDetails = "";
        pause.AddListener(Pause);
        retry.AddListener(Retry);
        exit.AddListener(Exit);
        fail.AddListener(Fail);
        fadeTarget = tint.color.a;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        for (float a = 1; a > 0; a -= Time.unscaledDeltaTime * 2)
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
            Instantiate(pauseMenu, transform).transform.SetSiblingIndex(transform.childCount - 2);
        }
        else
        {
            StartCoroutine(FadeTint(0));
            Destroy(GameObject.Find("PauseMenu(Clone)"));
        }
    }

    void Fail()
    {
        StartCoroutine(FadeTint(0.75f));
        GameObject menu = Instantiate(failMenu, transform);
        menu.transform.SetSiblingIndex(transform.childCount - 2);
        menu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = failDetails;
    }

    void Retry()
    {
        StartCoroutine(RetrySequence());
    }
    IEnumerator RetrySequence()
    {
        for (float a = 0; a < 1; a += Time.unscaledDeltaTime * 2)
        {
            yield return null;
            black.color = new(0, 0, 0, a);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Exit()
    {
        StartCoroutine(ExitSequence());
    }
    IEnumerator ExitSequence()
    {
        for (float a = 0; a < 1; a += Time.unscaledDeltaTime * 2)
        {
            yield return null;
            black.color = new(0, 0, 0, a);
        }
        SceneManager.LoadScene("Title Scene");
    }

    IEnumerator FadeTint(float target)
    {
        if (tint.color.a == fadeTarget)
        {
            fadeTarget = target;
            float fadeDirection = Mathf.Sign(fadeTarget - tint.color.a);
            while (fadeTarget * fadeDirection > tint.color.a * fadeDirection)
            {
                tint.color = new(0, 0, 0, tint.color.a + fadeDirection * Time.unscaledDeltaTime * 1.5f);
                yield return null;
                fadeDirection = Mathf.Sign(fadeTarget - tint.color.a);
            }
            tint.color = new(0, 0, 0, fadeTarget);
        }
        else
            fadeTarget = target;
    }
}
