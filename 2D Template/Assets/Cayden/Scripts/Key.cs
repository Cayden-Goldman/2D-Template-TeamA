using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Console.WriteLine("stinky");
        player.GetComponent<Vessel>().hasKey = true;
        Interactables.positions.Remove(new Vector2Int(MathF.Round(transform.position.x).ConvertTo<int>(), MathF.Round(transform.position.x).ConvertTo<int>()));
        Interactables.interactables.Remove(new Lung("Take", true));
        Destroy(gameObject);
    }
}
