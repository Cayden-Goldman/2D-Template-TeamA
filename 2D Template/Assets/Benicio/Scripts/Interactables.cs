using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static List<Interactable> interactables = new();

    public int objectId;

    void Start()
    {
        switch (objectId)
        {
            case 0:
                interactables.Add(new CrateGame("Search"));
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

    }
}