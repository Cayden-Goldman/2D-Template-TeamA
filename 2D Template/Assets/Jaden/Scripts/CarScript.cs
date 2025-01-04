using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    public static List<Vector3> objectPositions = new();

    public bool vertical;
    public bool key;
    public Material[] mats;
    [Range(1, 4)] public int length = 1;
    [HideInInspector] float halfDistance;

    Vector3 mousePos;
    Transform parent;
    float min, max;
    float offset;
    bool clicking;

    public void Start()
    {
        parent = transform.parent;
        SetMat();
        offset = 0.5f - (length % 2) / 2f;
        halfDistance = (length - 1) / 2f;
    }

    public void OnMouseDrag()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
        if (!clicking)
        {
            SetMat(true);
            GetObjectPositions();
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
            max -= offset;
            min += offset;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(mousePos).y + .5f) - offset, min, max), transform.position.z);
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
            max -= offset;
            min += offset;
            transform.position = new Vector3(Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(mousePos).x + .5f) - offset, min, max), transform.position.y, transform.position.z);
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
        {
            Transform child = parent.GetChild(i);
            if (child != transform)
            {
                CarScript childScript = child.GetComponent<CarScript>();
                Vector2 basePos = new Vector2(Mathf.Floor(child.position.x), Mathf.Floor(child.position.y));
                Vector2 dirVector;
                if (childScript.vertical) dirVector = Vector2.up;
                else dirVector = Vector2.right;
                if (childScript.length == 1) objectPositions.Add(basePos + new Vector2(0.5f, 0.5f));
                else
                {
                    Vector2 startPos = basePos - dirVector * Mathf.FloorToInt(childScript.halfDistance);
                    for (int t = 0; t < childScript.length; t++)
                        objectPositions.Add(startPos + dirVector * t + new Vector2(0.5f, 0.5f));
                }
            }
        }
    }

    void SetMat(bool clicking = false)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
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

    public void OnValidate()
    {
        if (vertical) transform.localScale = new(1, length);
        else transform.localScale = new(length, 1);
        SetMat();
    }
}
