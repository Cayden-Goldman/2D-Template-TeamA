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
                interactables.Add(new CrateGame("Searchmjui9"));
                break;
        }
    }
}

public abstract class Interactable
{
    public string text;
    public abstract void Interact();
}

public class CrateGame : Interactable
{

    public CrateGame(string text)
    {
        this.text = text;
    }

    public override void Interact()
    {
        CrateGame game = new CrateGame(text);
        game.Equals(this);
        BoxMinigame.evnt.Invoke();
    }
}