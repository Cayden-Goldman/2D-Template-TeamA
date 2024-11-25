using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public GameObject Object1;
    public GameObject Object2;
    public GameObject Object3;
    public GameObject Object4;
    public GameObject Object5;
    public GameObject Object6;

    private void Start()
    {
        OnButton0();
    }

    public void OnButton0()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(false);
        Object4.SetActive(false);
        Object5.SetActive(false);
        Object6.SetActive(false);
    }
    public void OnButton1()
    {
        Object1.SetActive(true);
        Object2.SetActive(false);
        Object3.SetActive(false);
        Object4.SetActive(false);
        Object5.SetActive(false);
        Object6.SetActive(false);
    }

    public void OnButton2()
    {
        Object1.SetActive(false);
        Object2.SetActive(true);
        Object3.SetActive(false);
        Object4.SetActive(false);
        Object5.SetActive(false);
        Object6.SetActive(false);
    }

    public void OnButton3()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(true);
        Object4.SetActive(false);
        Object5.SetActive(false);
        Object6.SetActive(false);
    }

    public void OnButton4()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(false);
        Object4.SetActive(true);
        Object5.SetActive(false);
        Object6.SetActive(false);
    }

    public void OnButton5()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(false);
        Object4.SetActive(false);
        Object5.SetActive(true);
        Object6.SetActive(false);
    }

    public void OnButton6()
    {
        Object1.SetActive(false);
        Object2.SetActive(false);
        Object3.SetActive(false);
        Object4.SetActive(false);
        Object5.SetActive(false);
        Object6.SetActive(true);
    }
}
