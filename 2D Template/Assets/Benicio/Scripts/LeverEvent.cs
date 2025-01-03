using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LeverEvent : MonoBehaviour
{
    public static UnityEvent evennt = new();
    bool aktiv = false;
    public GameObject door;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;

    Tilemap walls;
    void Start()
    {
        evennt.AddListener(OpenDoor);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }
    public void OpenDoor()
    {
        if(aktiv)
            ButtonPress.Door(door, walls, tilePositions, invisibleTile, aktiv = !aktiv);
        else
            ButtonPress.Door(door, walls, tilePositions, null, aktiv = !aktiv);
    }
}