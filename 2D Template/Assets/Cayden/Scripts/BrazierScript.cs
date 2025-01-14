using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BrazierScript : MonoBehaviour
{
    public static int braziersLit = 0;
    public GameObject[] doors;
    public void Light() 
    {
        GetComponentInChildren<Light2D>().enabled = !GetComponentInChildren<Light2D>().enabled;
        if (GetComponentInChildren<Light2D>().enabled)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            braziersLit++;
        }
        else
        {
            GetComponentInChildren<ParticleSystem>().Stop();
            braziersLit--;
        }
        switch (braziersLit)
        {
            case 4:
                doors[0].SetActive(false);
                break;
        }

    }
}
