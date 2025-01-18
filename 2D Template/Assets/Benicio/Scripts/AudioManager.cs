using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static GameObject obj;
    public static int[] loopChannels = new int[] { 0, 0, 0, 0 };
    //0 = textbox, 1 = ambience, 2 = music, 3 = footsteps

    void Start()
    {
        obj = gameObject;
        if (SceneManager.GetActiveScene().name == "guh")
            StartCoroutine(PlaySound("OutdoorAmbience", 1));
        else
            StartCoroutine(PlaySound("DreadfulWhispers", 1));
    }

    public static IEnumerator PlaySound(string soundName, int loopChannel = -1)
    {
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = Resources.Load("Sounds/" + soundName) as AudioClip;
        if (loopChannel < 0)
        {
            source.Play();
            yield return new WaitUntil(() => !source.isPlaying);
        }
        else
        {
            loopChannels[loopChannel] = 1;
            source.loop = true;
            source.Play();
            yield return new WaitUntil(() => loopChannels[loopChannel] == 2);
            loopChannels[loopChannel] = 0;
        }
        Destroy(source);
    }
}
