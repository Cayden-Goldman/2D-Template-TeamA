using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public Vector3 mouse;
    public GameObject ooooo;
    public List<Vector3> ooooo2;
    public bool vertical;
    public float min, max;
    public void Start()
    {
        Oooooo();
    }
    public void Update()
    {
        
        mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
    }
    public void OnMouseDrag()
    {
        Oooooo();
        if (vertical)
        {
            max = 2.5f;
            min = -3.5f;
            foreach(Vector3 t in ooooo2)
            {
                if(t.x == transform.position.x)
                {
                    if (t.y > transform.position.y && t.y < max) max = t.y - 1;
                    else if(t.y < transform.position.y && t.y > min) min = t.y + 1;
                }
            }
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).y + .5f) - .5f, min, max), transform.position.z);
        }
        else
        {
            transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).x + .5f) - .5f , transform.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -4.5f, 5.5f), transform.position.y, transform.position.z);
        }
    }
    public void Oooooo()
    {
        ooooo2.Clear();
        for (int i = 0; i < ooooo.transform.childCount; i++)
            if (ooooo.transform.GetChild(i) != this)
                ooooo2.Add(ooooo.transform.GetChild(i).position);
    }
}
