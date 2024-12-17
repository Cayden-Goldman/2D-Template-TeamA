using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public static Vector2Int pos;
    public static readonly List<string> passableTiles = new() { "baul", "CrateInner" };

    Tilemap walls;
    bool moving;
    Sprite currentSprite;
    SpriteRenderer sr;
    Animator animator;

    void Start()
    {
        pos = new((int)transform.position.x, (int)transform.position.y);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(UpdateShader());
    }

    void Update()
    {
        if (!moving)
        {
            if (Input.GetKey(KeyCode.A))
                StartCoroutine(Move(new(-1, 0)));
            else if (Input.GetKey(KeyCode.D))
                StartCoroutine(Move(new(1, 0)));
            else if (Input.GetKey(KeyCode.W))
                StartCoroutine(Move(new(0, 1)));
            else if (Input.GetKey(KeyCode.S))
                StartCoroutine(Move(new(0, -1)));
            else if (Input.GetKeyDown(KeyCode.Space) && pos == Vessel.pos)
            {
                Vessel.ghostMode = false;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Move(Vector2Int delta)
    {
        TileBase tile = walls.GetTile((Vector3Int)(pos + delta));
        bool canMove = false;
        if (tile == null) 
            canMove = true;
        else if (passableTiles.Contains(tile.name))
            canMove = true;
        if (canMove)
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

    IEnumerator UpdateShader()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentSprite != sr.sprite);
            currentSprite = sr.sprite;
            sr.material.SetTexture("_Texture", currentSprite.texture);
        }
    }
}
