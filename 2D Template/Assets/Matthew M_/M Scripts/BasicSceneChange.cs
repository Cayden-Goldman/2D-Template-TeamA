using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicSceneChange : MonoBehaviour
{
    public string scenename;
    public GameObject Image;
    public float WaitTime;

    public void Awake()
    {
        Image.SetActive(false);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenename);
    }
    public void UniqueExit()
    {
        Invoke("ChangeScene", WaitTime);
        Image.SetActive(true);
    }

    public void End()
    {
        Application.Quit();
    }
}
