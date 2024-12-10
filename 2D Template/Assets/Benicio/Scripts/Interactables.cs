using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static List<Vector2Int> positions = new();
    public static List<Interactable> interactables = new();

    public int objectId;

    void Start()
    {
        positions.Add(new((int)transform.position.x, (int)transform.position.y));
        switch (objectId)
        {
            case 0:
                interactables.Add(new CrateGame("Search", true));
                break;
        }
    }
}

public abstract class Interactable
{
    public string text;
    public bool ghostOnly;
    public abstract void Interact();
}

public class CrateGame : Interactable
{
    public CrateGame(string text, bool ghostOnly = false)
    {
        this.text = text;
    }

    public override void Interact()
    {
        BoxMinigame.evnt.Invoke();
    }
}