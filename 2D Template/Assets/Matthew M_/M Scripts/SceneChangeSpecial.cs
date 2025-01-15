using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeSpecial : MonoBehaviour
{
    public string thescene;
    public float wait;
    void Start()
    {
        StartCoroutine(IntroSwitch());
    }

    IEnumerator IntroSwitch()
    {
        yield return new WaitForSeconds(wait);
        SceneManager.LoadScene(thescene);
    }
}
