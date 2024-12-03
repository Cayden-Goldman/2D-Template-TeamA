using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMinigame : MonoBehaviour
{
    public GameObject player, boxGame, collidables;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
            Minigame();
    }
    void Minigame()
    {
        player.GetComponent<Vessel>().enabled = false;
        collidables.SetActive(false);
        boxGame.SetActive(true);
    }
}
