using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Interactables : MonoBehaviour
{
    public static List<Vector2Int> positions = new();
    public static List<Interactable> interactables = new();

    public enum Objects { 
    boxGame,
    lever,
    key,
    lockedDoor,
    npc,
    brazier};

    public Objects obj;

    void Start()
    {
        positions.Add(new((int)transform.position.x, (int)transform.position.y));
        switch (obj)
        {
            case 0:
                interactables.Add(new CrateGame("Search", gameObject, true));
                break;
            case (Objects)1:
                interactables.Add(new Lever("Pull", gameObject));
                break;
            case (Objects)2:
                interactables.Add(new KeyItem("Take", gameObject));
                break;
            case (Objects)3:
                interactables.Add(new LockedDoor("Unlock", gameObject, false, true));
                break;
            case (Objects)4:
                interactables.Add(new Guard("Talk", gameObject, false, true));
                break;
            case (Objects)5:
                interactables.Add(new Brazier("Light", gameObject));
                break;
        }
    }
    
    public static void RemoveInteractable(GameObject obj)
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i].obj == obj)
            {
                interactables.RemoveAt(i);
                positions.RemoveAt(i);
                break;
            }
        }
    }
}

public abstract class Interactable
{
    public string text;
    public bool ghostOnly;
    public bool vesselOnly;
    public GameObject obj;
    public abstract void Interact();
}

public class CrateGame : Interactable
{
    public CrateGame(string text, GameObject obj , bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<BoxMinigame>().Minigame();
    }
}

public class Lever : Interactable
{
    public Lever(string text, GameObject obj, bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<LeverEvent>().OpenDoor();
    }
}

public class KeyItem : Interactable
{
    public KeyItem(string text, GameObject obj, bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }

    public override void Interact()
    {
        Key.evenn.Invoke();
    }
}

public class LockedDoor : Interactable
{
    public LockedDoor(string text, GameObject obj, bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<Door>().Open();
    }
}

public class Guard : Interactable
{
    public Guard(string text, GameObject obj, bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<Npc>().Interact();
    }
}

public class Brazier : Interactable
{
    public Brazier(string text, GameObject obj, bool ghostOnly = false, bool vesselOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
        this.vesselOnly = vesselOnly;
    }
    public override void Interact()
    {
        obj.GetComponent<BrazierScript>().Light();
    }
}