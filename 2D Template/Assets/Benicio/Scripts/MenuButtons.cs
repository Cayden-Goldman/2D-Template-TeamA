using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void Resume()
    {
        Vessel.paused = false;
        UiManager.pause.Invoke();
        Time.timeScale = 1;
    }

    public void Retry()
    {
        UiManager.retry.Invoke();
    }

    public void ExitToMenu()
    {
        UiManager.exit.Invoke();
    }
}
