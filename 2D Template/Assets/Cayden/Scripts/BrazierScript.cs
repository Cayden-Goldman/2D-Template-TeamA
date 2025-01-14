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
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponentInChildren<Light2D>().enabled = true;
        braziersLit++;
        
        switch (braziersLit)
        {
            case 4:
                doors[0].SetActive(false);
                break;
        }

    }
}
