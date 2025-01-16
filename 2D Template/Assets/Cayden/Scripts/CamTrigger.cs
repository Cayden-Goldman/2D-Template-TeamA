using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject camFollow;
    public ParalaxThing thing;
    bool moved = false;
    public Vector3 endPos;
    public Vector3 startPos;
    public bool horizontal;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Ghost"))
        {
            if(moved)
            {
                thing.ogCamPos = startPos;
                camFollow.transform.position = startPos;
                moved = false;
            }
            else
            {
                thing.ogCamPos = endPos;
                camFollow.transform.position = endPos;
                moved = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(horizontal && collision.gameObject.transform.position.x < transform.position.x)
        {
            camFollow.transform.position = endPos;
            thing.ogCamPos = endPos;
        }
        else if (horizontal && collision.gameObject.transform.position.x > transform.position.x)
        {
            camFollow.transform.position = startPos;
            thing.ogCamPos = startPos;
        }
        else if (!horizontal && collision.gameObject.transform.position.y < transform.position.y)
        {
            camFollow.transform.position = startPos;
            thing.ogCamPos = startPos;
        }
        else if (!horizontal && collision.gameObject.transform.position.y > transform.position.y)
        {
            camFollow.transform.position = endPos;
            thing.ogCamPos = endPos;
        }
    }
}
