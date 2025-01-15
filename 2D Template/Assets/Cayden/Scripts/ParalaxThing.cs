using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxThing : MonoBehaviour
{
    public GameObject player;
    public Vector3 ogCamPos;
    Vector3 position;
    private void Start()
    {
        ogCamPos = transform.position;
    }
    void Update()
    {
        if(player.transform.position.y < ogCamPos.y)
            position.y = ogCamPos.y - 1;
        else if(player.transform.position.y > ogCamPos.y)
            position.y = ogCamPos.y + 3;
        transform.position = new Vector3(transform.position.x, position.y, transform.position.z);
    }
}
