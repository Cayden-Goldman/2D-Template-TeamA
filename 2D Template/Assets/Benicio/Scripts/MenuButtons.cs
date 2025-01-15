using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
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
}
