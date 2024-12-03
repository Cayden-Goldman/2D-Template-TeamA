using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public static List<Vector2Int> positions = new();
    public static List<GameObject> objects = new();

    void Start()
    {
        positions.Add(new((int)transform.position.x, (int)transform.position.y));
        objects.Add(gameObject);
    }
}
