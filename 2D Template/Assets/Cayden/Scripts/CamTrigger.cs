using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject camFollow;
    public ParalaxThing thing;
    bool up = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            switch(up)
            {
                case true:
                    thing.ogCamPos.y -= 14;
                    camFollow.transform.position = new Vector3(0, 0, -10); 
                    up = false; 
                    break;
                case false:
                    thing.ogCamPos.y += 14;
                    camFollow.transform.position = new Vector3(0, 14, -10);
                    up = true;
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.y < transform.position.y)
            camFollow.transform.position = new Vector3(0, 0, -10);
        else if (collision.gameObject.transform.position.y > transform.position.y)
            camFollow.transform.position = new Vector3(0, 11, -10);
    }
}
