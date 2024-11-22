using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2Int pos;
    bool moving;

    void Start()
    {
        pos = new((int)transform.position.x, (int)transform.position.y);
    }

    void Update()
    {
        if (!moving)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                StartCoroutine(Move(new(-1, 0)));
            else if (Input.GetKey(KeyCode.RightArrow))
                StartCoroutine(Move(new(1, 0)));
            else if (Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(Move(new(0, 1)));
            else if (Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(Move(new(0, -1)));
        }
    }

    IEnumerator Move(Vector2Int delta)
    {
        moving = true;
        Vector2Int startPos = pos;
        pos += delta;
        for (float t = 0; t < 1; t += Time.deltaTime * 6)
        {
            transform.position = Vector2.Lerp(startPos, pos, t);
            yield return null;
            Debug.Log(transform.position + ": " + t);
        }
        moving = false;
    }
}
