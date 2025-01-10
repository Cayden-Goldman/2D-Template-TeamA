using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public enum Directions
    {
        up, right, down, left
    }

    public string guardName;
    public string dialogue;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact()
    {
        TextBox.AddDialogue(guardName, dialogue, true);
    }
}
