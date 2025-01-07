using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public static List<Vector2Int> positions = new();
    public static List<Interactable> interactables = new();

    public enum Objects { 
    boxGame,
    lever,
    lung,
    mind};

    public Objects obj;

    void Start()
    {
        positions.Add(new((int)transform.position.x, (int)transform.position.y));
        switch (obj)
        {
            case 0:
                interactables.Add(new CrateGame("Search", true));
                break;
            case (Objects)1:
                interactables.Add(new Lever("Pull", true));
                break;
            case (Objects)2:
                interactables.Add(new Lung("Take", true));
                break;
            case (Objects)3:
                interactables.Add(new Mind("Door", true));
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

public class Lever : Interactable
{
    
    public Lever(string text, bool ghostOnly = false)
    {
        this.text = text;
    }

    public override void Interact()
    {
        LeverEvent.evennt.Invoke();
    }
}
public class Lung : Interactable
{

    public Lung(string text, bool ghostOnly = false)
    {
        this.text = text;
    }

    public override void Interact()
    {
        Key.evenn.Invoke();
    }
}
public class Mind : Interactable
{
    public Mind(string text, bool ghostOnly = false)
    {
        this.text = text;
    }

    public override void Interact()
    {
        Door.evetn.Invoke();
    }
}