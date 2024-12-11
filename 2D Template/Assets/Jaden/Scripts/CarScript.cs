using System.Collections;
using System.Collections.Generic;
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
            
        }
        else
        transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).x + .5f) - .5f , transform.position.y, transform.position.z);
    }
}
