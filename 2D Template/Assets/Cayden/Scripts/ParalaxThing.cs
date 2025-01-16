using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxThing : MonoBehaviour
{
    public GameObject player;
    GameObject ghost;
    public Vector3 ogCamPos;
    Vector3 position;
    private void Start()
    {
        ogCamPos = transform.position;
    }
    void Update()
    {
        if (Vessel.ghostMode == true)
            ghost = GameObject.Find("Ghost(Clone)");
        else
            ghost = player;
        if(ghost.transform.position.y < ogCamPos.y )
            position.y = ogCamPos.y - 1;
        else if(ghost.transform.position.y > ogCamPos.y)
            position.y = ogCamPos.y + 3;
        if (ghost.transform.position.x < ogCamPos.x - 0.5f)
            position.x = ogCamPos.x - 1;
        else if (ghost.transform.position.x > ogCamPos.x - 0.5f)
            position.x = ogCamPos.x + 1;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
