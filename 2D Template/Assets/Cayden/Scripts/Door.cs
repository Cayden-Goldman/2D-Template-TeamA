using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        foreach (Vector2Int pos in tilePositions)
            Interactables.positions.Remove(pos);
        Interactables.interactables.Remove(new Mind("Door", true));
        if(player.GetComponent<Vessel>().hasKey)
        {
            ButtonPress.Door(gameObject, walls, tilePositions, null, true);
        }
    }
}
