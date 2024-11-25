using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vessel : MonoBehaviour
{
    public static Vector2Int pos;
    public static bool ghostMode;

    public GameObject ghostObj;

    Tilemap walls;
    bool moving;

    void Start()
    {
        pos = new((int)transform.position.x, (int)transform.position.y);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
    }

    void Update()
    {
        if (!moving && !ghostMode)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                StartCoroutine(Move(new(-1, 0)));
            else if (Input.GetKey(KeyCode.RightArrow))
                StartCoroutine(Move(new(1, 0)));
            else if (Input.GetKey(KeyCode.UpArrow))
                StartCoroutine(Move(new(0, 1)));
            else if (Input.GetKey(KeyCode.DownArrow))
                StartCoroutine(Move(new(0, -1)));
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                ghostMode = true;
                Instantiate(ghostObj, new Vector3(pos.x, pos.y), new());
            }
        }
    }

    IEnumerator Move(Vector2Int delta)
    {
        TileBase tile = walls.GetTile((Vector3Int)(pos + delta));
        if (tile == null)
        {
            moving = true;
            Vector2Int startPos = pos;
            pos += delta;
            for (float t = 0; t < 1; t += Time.deltaTime * 6)
            {
                transform.position = Vector2.Lerp(startPos, pos, t);
                yield return null;
            }
            moving = false;
        }
    }
}
