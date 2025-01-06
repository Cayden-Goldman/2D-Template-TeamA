using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Key : MonoBehaviour
{
    public static UnityEvent evenn = new();
    public GameObject player;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;

    Tilemap walls;
    void Start()
    {
        evenn.AddListener(GrabKey);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }
    public void GrabKey()
    {
        player.GetComponent<Vessel>().hasKey = true;
        Destroy(gameObject);
    }
}
