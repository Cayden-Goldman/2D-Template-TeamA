using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSpecial : MonoBehaviour
{
    public string thescene;
    void Start()
    {
        StartCoroutine(IntroSwitch());
    }

    IEnumerator IntroSwitch()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(thescene);
    }
}
