using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarScript : MonoBehaviour, IPointerEnterHandler
{
    public Vector3 mouse;
    public bool vertical;
    public void Update()
    {
        mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
    }
    public void OnMouseDrag()
    {
        Debug.Log("Hi");
        if(vertical)
        transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(mouse).y, transform.position.z);
        else
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mouse).x, transform.position.y, transform.position.z);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }
}
