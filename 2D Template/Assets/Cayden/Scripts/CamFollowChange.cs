using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowChange : MonoBehaviour
{
    public GameObject followThing, camera;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            camera.GetComponent<CinemachineVirtualCamera>().Follow = followThing.transform;
        }
    }
}
