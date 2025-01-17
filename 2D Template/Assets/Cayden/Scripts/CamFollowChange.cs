using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowChange : MonoBehaviour
{
    public GameObject followThing, cam;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ghost"))
        {
            cam.GetComponent<CinemachineVirtualCamera>().Follow = followThing.transform;
        }
    }
}
