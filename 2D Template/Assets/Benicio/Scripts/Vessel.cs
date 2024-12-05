using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vessel : MonoBehaviour
{
    public static Vector2Int pos;
    public static bool ghostMode;

    public GameObject ghostObj;
    public Animator animator;

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
            if (Input.GetKey(KeyCode.S))
            {
                StartCoroutine(Move(new(0, -1)));
                animator.SetInteger("Direction", 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Move(new(-1, 0))); 
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                StartCoroutine(Move(new(0, 1)));
                animator.SetInteger("Direction", 2);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Move(new(1, 0)));
                animator.SetInteger("Direction", 3);
            }
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
            if (Movable.positions.Contains(pos + delta))
            {
                tile = walls.GetTile((Vector3Int)(pos + delta * 2));
                if (tile == null)
                {
                    GameObject obj = Movable.objects[Movable.positions.IndexOf(pos + delta)];
                    Movable.positions[Movable.positions.IndexOf(pos + delta)] += delta;
                    moving = true;
                    Vector2Int startPos = pos;
                    pos += delta;
                    for (float t = 0; t < 1; t += Time.deltaTime * 6)
                    {
                        transform.position = Vector2.Lerp(startPos, pos, t);
                        obj.transform.position = transform.position + (Vector3)(Vector2)delta;
                        yield return 3;
                    }
                }
            }
            else
            {
                moving = true;
                animator.SetBool("IsWalking", true);
                Vector2Int startPos = pos;
                pos += delta;
                for (float t = 0; t < 1; t += Time.deltaTime * 6)
                {
                    transform.position = Vector2.Lerp(startPos, pos, t);
                    yield return null;
                }
                animator.SetBool("IsWalking", false);
            }
            moving = false;
        }
    }
}
