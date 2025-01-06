using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public static UnityEvent evetn = new();
    public GameObject player;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;

    Tilemap walls;
    void Start()
    {
        evetn.AddListener(Open);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }
    public void Open()
    {
        if(player.GetComponent<Vessel>().hasKey)
        {
            Destroy(gameObject);
        }
    }
}
