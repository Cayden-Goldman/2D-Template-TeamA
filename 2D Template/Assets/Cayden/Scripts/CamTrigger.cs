using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject camFollow;
    public ParalaxThing thing;
    bool up = false;
    public Vector3 endPos;
    public Vector3 startPos;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            switch(up)
            {
                case true:
                    thing.ogCamPos.y -= endPos.y;
                    camFollow.transform.position = startPos; 
                    up = false; 
                    break;
                case false:
                    thing.ogCamPos.y += endPos.y;
                    camFollow.transform.position = endPos;
                    up = true;
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.y < transform.position.y)
            camFollow.transform.position = startPos;
        else if (collision.gameObject.transform.position.y > transform.position.y)
            camFollow.transform.position = endPos;
    }
}
