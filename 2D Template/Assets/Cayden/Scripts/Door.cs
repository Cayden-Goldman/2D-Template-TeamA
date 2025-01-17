using System;
using System.Collections;
using UnityEngine;
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
            Interactables.RemoveInteractable(gameObject);
            ButtonPress.Door(doorPart, walls, tilePositions, null, true);
        }
    }
}
