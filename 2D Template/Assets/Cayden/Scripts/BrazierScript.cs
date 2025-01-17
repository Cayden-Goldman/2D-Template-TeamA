using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class BrazierScript : MonoBehaviour
{
    public static int braziersLit = 0;
    public GameObject[] doors;
    public Vector2Int[] tilePositions;
    public TileBase invisibleTile;
    Tilemap walls;
    private void Start()
    {
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        foreach (Vector2Int pos in tilePositions)
            walls.SetTile((Vector3Int)pos, invisibleTile);
    }
    public void Light() 
    {
        GetComponentInChildren<ParticleSystem>().Play();
        GetComponentInChildren<Light2D>().enabled = true;
        braziersLit++;
        
        switch (braziersLit)
        {
            case 4:
                ButtonPress.Door(doors[0], walls, tilePositions, null, true);
                break;
            case 5:
                ButtonPress.Door(doors[1], walls, tilePositions, null, true);
                break;
        }

    }
}
