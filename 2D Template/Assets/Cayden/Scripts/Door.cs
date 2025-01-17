using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public GameObject player;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;
    public bool keyObtained = false;
    public GameObject doorPart;

    Tilemap walls;
    void Start()
    {
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }
    public void Open()
    {
        if(keyObtained)
        {
            ButtonPress.Door(doorPart, walls, tilePositions, null, true);
            foreach (Vector2Int pos in tilePositions)
                Interactables.positions.Remove(pos);
            Interactables.interactables.Remove(new LockedDoor("Door", gameObject, true));
        }
    }
}
