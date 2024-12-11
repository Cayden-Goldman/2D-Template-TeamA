using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverEvent : MonoBehaviour
{
    public static UnityEvent evennt = new();
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        evennt.AddListener(OpenDoor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        bool aktiv = true;
        if (aktiv)
        {
            door.SetActive(false);
            aktiv = false;
        }
        else
        {
            door.SetActive(true);
            aktiv = true;
        }
    }


}
