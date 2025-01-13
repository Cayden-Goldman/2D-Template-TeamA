using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Npc : MonoBehaviour
{
    public enum Direction
    {
        down, left, up, right
    }

    public string guardName;
    public string dialogue;
    public Direction direction;
    public TileBase dangerTile;

    Tilemap walls;
    Tilemap dangerTiles;

    void Start()
    {
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        dangerTiles = GameObject.Find("DangerTiles").GetComponent<Tilemap>();
        SetSightBounds();
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        TextBox.AddDialogue(guardName, dialogue, true);
    }

    public void SetSightBounds()
    {
        Vector3Int startPos = new((int)transform.position.x, (int)transform.position.y);
        for (int e = -3; e <= 3; e++)
        {
            for (int d = 1; d < 20; d++)
            {
                Vector3Int pos = startPos + new Vector3Int(-d, Mathf.FloorToInt(d / 20f * e));
                if (walls.GetTile(pos) == null)
                    dangerTiles.SetTile(pos, dangerTile);
                else break;
            }
        }
    }
}
