using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInInactive : MonoBehaviour
{
    public GameObject Yay;
    IEnumerator StopPlease()
    {
        yield return new WaitForSeconds(1f);
        Yay.SetActive(false);
    }
    void Start()
    {
        Yay.SetActive(true);
        StartCoroutine(StopPlease());
    }
}
