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
                interactables.Add(new CrateGame("Search", true));
                break;
            case (Objects)1:
                interactables.Add(new Lever("Pull", gameObject));
                break;
            case (Objects)2:
                interactables.Add(new KeyItem("Take"));
                break;
            case (Objects)3:
                interactables.Add(new LockedDoor("Unlock"));
                break;
            case (Objects)4:
                interactables.Add(new Guard("Talk", gameObject));
                break;
            case (Objects)5:
                interactables.Add(new Brazier("Light", gameObject));
                break;
        }
    }
}

public abstract class Interactable
{
    public string text;
    public bool ghostOnly;
    public GameObject obj;
    public abstract void Interact();
}

public class CrateGame : Interactable
{
    public CrateGame(string text, bool ghostOnly = false)
    {
        this.text = text;
        this.ghostOnly = ghostOnly;
    }

    public override void Interact()
    {
        BoxMinigame.evnt.Invoke();
    }
}

public class Lever : Interactable
{
    public Lever(string text, GameObject obj, bool ghostOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<LeverEvent>().OpenDoor();
    }
}

public class KeyItem : Interactable
{
    public KeyItem(string text, bool ghostOnly = false)
    {
        this.text = text;
        this.ghostOnly = ghostOnly;
    }

    public override void Interact()
    {
        Key.evenn.Invoke();
    }
}

public class LockedDoor : Interactable
{
    public LockedDoor(string text, bool ghostOnly = false)
    {
        this.text = text;
        this.ghostOnly = ghostOnly;
    }

    public override void Interact()
    {
        Door.evetn.Invoke();
    }
}

public class Guard : Interactable
{
    public Guard(string text, GameObject obj, bool ghostOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
    }

    public override void Interact()
    {
        obj.GetComponent<Npc>().Interact();
    }
}

public class Brazier : Interactable
{
    public Brazier(string text, GameObject obj, bool ghostOnly = false)
    {
        this.text = text;
        this.obj = obj;
        this.ghostOnly = ghostOnly;
    }
    public override void Interact()
    {
        obj.GetComponentInChildren<Light2D>().enabled = !obj.GetComponentInChildren<Light2D>().enabled;
        if(obj.GetComponentInChildren<Light2D>().enabled)
            obj.GetComponentInChildren<ParticleSystem>().Play();
        else
            obj.GetComponentInChildren<ParticleSystem>().Stop();

    }
}