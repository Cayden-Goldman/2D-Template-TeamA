using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxMinigame : MonoBehaviour
{
    public GameObject player, boxGame, collidables, text;
    public GameObject key;
    public void Minigame()
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
