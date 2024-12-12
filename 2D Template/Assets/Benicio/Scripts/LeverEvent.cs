using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverEvent : MonoBehaviour
{
    public static UnityEvent evennt = new();
    public GameObject door;
    bool aktiv = true;
    void Start()
    {
        evennt.AddListener(OpenDoor);
    }
    public void OpenDoor()
    {
            door.SetActive(aktiv = !aktiv);
    }
}