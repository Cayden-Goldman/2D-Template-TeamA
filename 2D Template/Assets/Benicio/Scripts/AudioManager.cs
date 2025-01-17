using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static GameObject obj;
    public static bool[] loopChannels = new bool[] { false, false, false, false, false };

    void Start()
    {
        obj = gameObject;
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
            source.loop = true;
            source.Play();
            yield return new WaitUntil(() => loopChannels[loopChannel]);
            loopChannels[loopChannel] = false;
        }
        Destroy(source);
    }
}
