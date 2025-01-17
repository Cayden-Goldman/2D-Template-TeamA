using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Npc : MonoBehaviour
{
    public static UnityEvent playerFound = new();

    public enum Direction
    {
        down, left, up, right
    }

    public string guardName;
    public string dialogue;
    public Direction direction;
    public TileBase dangerTile;
    public GameObject alert;

    Tilemap walls;
    Tilemap dangerTiles;
    List<Vector2Int> tilePos = new();

    void Start()
    {
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        dangerTiles = GameObject.Find("DangerTiles").GetComponent<Tilemap>();
        GetComponent<Animator>().SetInteger("Direction", (int)direction);
        SetSightBounds();
        playerFound.AddListener(CheckSight);
    }

    public void Interact()
    {
        TextBox.AddDialogue(guardName, dialogue, true);
    }

    void SetSightBounds()
    {
        Vector3Int startPos = new((int)transform.position.x, (int)transform.position.y);
        for (int e = -5; e <= 5; e++)
        {
            for (int d = 0; d < 20; d++)
            {
                Vector3Int pos = startPos + (Vector3Int)Vessel.directions[(int)direction] * d;
                if ((int)direction % 2 == 1) 
                    pos += new Vector3Int(0, Mathf.RoundToInt(d / 20f * e));
                else
                    pos += new Vector3Int(Mathf.RoundToInt(d / 20f * e), 0);
                if (walls.GetTile(pos) == null)
                {
                    dangerTiles.SetTile(pos, dangerTile);
                    tilePos.Add((Vector2Int)pos);
                }
                else break;
            }
        }
    }

    void CheckSight()
    {
        if (tilePos.Contains(Ghost.pos))
            Instantiate(alert, transform.position + new Vector3(0.5f, 3), new());
    }
}
