using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CarScript : MonoBehaviour
{
    public static List<Vector3> objectPositions = new();

    public enum Objects
    {
        boxBoye,
        key,
        goldBarVertical,
        coinVertical,
        goldBarHorizontal,
        coinHorizontal
    };

    public Objects obj;

    [Range(1, 4)] public int length = 1;
    public bool vertical;
    public bool key;
    public Material[] mats;

    Vector3 mousePos;
    Transform parent;

    SpriteRenderer sr;
    float min, max;
    float offset;
    float halfDistance;
    bool clicking;

    public void Start()
    {
        parent = transform.parent;
        SetMat(sr = GetComponent<SpriteRenderer>());
        offset = 0.5f - (length % 2) / 2f;
        halfDistance = (length - 1) / 2f;
    }

    public void OnMouseDrag()
    {
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, .1f);
        if (!clicking)
        {
            SetMat(sr, true);
            GetObjectPositions();
        }
        clicking = true;
        float sizeOffset = Mathf.Floor((length - 1) / 2f);
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
            max -= offset + sizeOffset;
            min += offset + sizeOffset;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(mousePos).y + .5f) - .5f + offset, min, max), transform.position.z);
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
            if(key)
            {
                if(transform.position.x == 7)
                {
                    GameObject gameObject = GameObject.Find("Crate");
                    gameObject.GetComponent<BoxMinigame>().MinigameEnd();
                }
            }
            max -= offset + sizeOffset;
            min += offset + sizeOffset;
            transform.position = new Vector3(Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(mousePos).x + .5f) - .5f + offset, min, max), transform.position.y, transform.position.z);
        }
    }

    public void OnMouseUp()
    {
        clicking = false;
        SetMat(sr);
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
                Vector2 basePos = child.position;
                Vector2 dirVector;
                if (childScript.vertical) dirVector = Vector2.up;
                else dirVector = Vector2.right;
                if (childScript.length == 1) objectPositions.Add(basePos);
                else
                {
                    Vector2 startPos = basePos - dirVector * childScript.halfDistance;
                    for (int t = 0; t < childScript.length; t++)
                        objectPositions.Add(startPos + dirVector * t);
                }
            }
        }
    }

    void SetMat(SpriteRenderer sr, bool clicking = false)
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
        sr.material.SetTexture("_Texture", sr.sprite.texture);
    }

    public void OnValidate()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D col = sr.GetComponent<BoxCollider2D>();
        if (obj == Objects.boxBoye)
        {
            if (vertical) transform.localScale = new(1, length);
            else transform.localScale = new(length, 1);
            col.size = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.one;
            if (vertical) col.size = new(1, length);
            else col.size = new(length, 1);
        }
        switch (obj)
        {
            default:
                sr.sprite = Resources.Load<Sprite>("CrateGame/BoxBoye");
                if (vertical) transform.localScale = new(1, length);
                else transform.localScale = new(length, 1);
                break;
            case Objects.key:
                sr.sprite = Resources.Load<Sprite>("CrateGame/Key");
                vertical = false;
                key = true;
                length = 2;
                break;
            case Objects.goldBarVertical:
                length = Mathf.Clamp(length, 2, 4);
                sr.sprite = Resources.Load<Sprite>("CrateGame/GoldBarVertical" + length);
                vertical = true;
                key = false;
                break;
            case Objects.coinVertical:
                length = 1;
                sr.sprite = Resources.Load<Sprite>("CrateGame/CoinVertical");
                vertical = true;
                key = false;
                break;
            case Objects.goldBarHorizontal:
                length = Mathf.Clamp(length, 2, 4);
                sr.sprite = Resources.Load<Sprite>("CrateGame/GoldBarHorizontal" + length);
                vertical = false;
                key = false;
                break;
            case Objects.coinHorizontal:
                length = 1;
                sr.sprite = Resources.Load<Sprite>("CrateGame/CoinHorizontal");
                vertical = false;
                key = false;
                break;
        }
    }
}
