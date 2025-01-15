using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxMinigame : MonoBehaviour
{
    public static UnityEvent evnt = new();
    public GameObject player, boxGame, collidables, text;
    public GameObject key;
    private void Start()
    {
        evnt.AddListener(Minigame);
    }
    void Minigame()
    {
        Vessel.paused = true;
        Vessel.canMove = false;
        text.SetActive(false);   
        boxGame.SetActive(true);
    }
    public void MinigameEnd()
    {
        Vessel.paused = false;
        Vessel.canMove = true;
        boxGame.SetActive(false);
        key.SetActive(true);
    }
}
