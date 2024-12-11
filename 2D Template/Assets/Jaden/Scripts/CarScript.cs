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
    public void Start()
    {
        for(int i = 0; i < ooooo.transform.childCount; i++)
            if (ooooo.transform.GetChild(i) != this)
                ooooo2.Add(ooooo.transform.GetChild(i).position);
    }
    public void Update()
    {
        
        mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
    }
    public void OnMouseDrag()
    {

        if (vertical)
        {
            float max, min;
            foreach(Vector3 t in ooooo2)
            {
                if(t.x == transform.transform.transform.transform.transform.transform.position.x)
                {
                    if (t.y > transform.transform.transform.position.y)
                    {
                       MaybeNullWhenAttribute nullWhenAttribute = null; 
                    }
                }
            }
        }
        else
        transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).x + .5f) - .5f , transform.position.y, transform.position.z);
    }
}
