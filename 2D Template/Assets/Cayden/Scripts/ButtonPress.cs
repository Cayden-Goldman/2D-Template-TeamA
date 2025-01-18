using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonPress : MonoBehaviour
{
    public GameObject door;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;
    
    Tilemap walls;

    void Start()
    {
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            StartCoroutine(AudioManager.PlaySound("ButtonPress"));
            GetComponent<Animator>().SetBool("IsPressed", true);
            Door(door, walls, tilePositions, null, true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            GetComponent<Animator>().SetBool("IsPressed", false);
            Door(door, walls, tilePositions, invisibleTile, false);
        }
    }

    public static void Door(GameObject door, Tilemap tilemap, Vector2Int[] tiles, TileBase tile, bool open)
    {
        foreach (Vector2Int pos in tiles)
            tilemap.SetTile((Vector3Int)pos, tile);
        door.SetActive(!open);
    }
}
