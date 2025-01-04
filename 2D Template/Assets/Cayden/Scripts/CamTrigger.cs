using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject camFollow;
    bool up = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            switch(up)
            {
                case true: 
                    camFollow.transform.position = new Vector3(0, 0, -10); 
                    up = false; 
                    break;
                case false:
                    camFollow.transform.position = new Vector3(0, 11, -10);
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
