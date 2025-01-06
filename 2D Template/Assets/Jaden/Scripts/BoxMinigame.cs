using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxMinigame : MonoBehaviour
{
    public static UnityEvent evnt = new();
    public GameObject player, boxGame, collidables, text;
    private void Start()
    {
        evnt.AddListener(Minigame);
    }
    void Minigame()
    {
        player.GetComponent<Vessel>().enabled = false;
        collidables.SetActive(false);
        text.SetActive(false);   
        boxGame.SetActive(true);
    }
    public void MinigameEnd()
    {
        player.GetComponent<Vessel>().enabled = true;
        collidables.SetActive(true);
        boxGame.SetActive(false);
        
    }
}
