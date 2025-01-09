using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
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
