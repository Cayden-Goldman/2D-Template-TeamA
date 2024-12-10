using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarScript : MonoBehaviour, IPointerEnterHandler
{
    public Vector3 mouse;
    public GameObject[] gameObjects;
    public bool vertical;
    public void Update()
    {
        mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
    }
    public void OnMouseDrag()
    {
        if(vertical)
        transform.position = new Vector3(transform.position.x, Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).y + .5f) - .5f, transform.position.z);
        else
        transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(mouse).x + .5f) - .5f , transform.position.y, transform.position.z);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("The cursor entered the selectable UI element.");
    }
}
