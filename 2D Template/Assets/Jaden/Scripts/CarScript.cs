using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    public static List<Vector3> objectPositions;

    public Vector3 mouse;
    public bool vertical;
    public bool key;
    public float min, max;
    public Material[] mats;

    Transform parent;
    SpriteRenderer sr;
    bool clicking;

    public void Start()
    {
        parent = transform.parent;
        sr = GetComponent<SpriteRenderer>();
        SetMat();
        if (key) GetObjectPositions();
    }

    public void Update()
    {
        mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
    }

    public void OnMouseDrag()
    {
        GetObjectPositions();
        if (!clicking)
        {
            SetMat(true);
        }
        clicking = true;
        if (vertical)
        {
            max = 2.5f;
            min = -3.5f;
            foreach (Vector3 t in objectPositions)
            {
                if (t.x == transform.position.x)
                {
                    if (t.y > transform.position.y && t.y <= max) max = t.y - 1;
                    else if (t.y < transform.position.y && t.y >= min) min = t.y + 1;
                }
            }
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Round(Camera.main.GetComponent<Camera>().GetComponent<Camera>().ScreenToWorldPoint(mouse).y + .5f) - .5f, min, max), transform.position.z);
        }
        else
        {
            max = 5.5f;
            min = -4.5f;
            if (key) max = 7.5f;
            foreach (Vector3 t in objectPositions)
            {
                if (t.y == transform.position.y)
                {
                    if (t.x > transform.position.x && t.x <= max) max = t.x - 1;
                    else if (t.x < transform.position.x && t.x >= min) min = t.x + 1;
                }
            }
            transform.position = new Vector3(Mathf.Clamp(Mathf.Round(Camera.main.GetComponent<Camera>().GetComponent<Camera>().ScreenToWorldPoint(mouse).x + .5f) - .5f, min, max), transform.position.y, transform.position.z);
        }
    }

    public void OnMouseUp()
    {
        clicking = false;
        SetMat();
    }

    public void GetObjectPositions()
    {
        objectPositions.Clear();
        for (int i = 0; i < parent.childCount; i++)
            if (parent.GetChild(i) != this)
                objectPositions.Add(parent.GetChild(i).position);
    }

    void SetMat(bool clicking = false)
    {
        if (clicking)
        {
            if (key)
                sr.material = mats[5];
            else if (vertical)
                sr.material = mats[3];
            else
                sr.material = mats[4];
        }
        else
        {
            if (key)
                sr.material = mats[2];
            else if (vertical)
                sr.material = mats[0];
            else
                sr.material = mats[1];
        }    
    }
}
