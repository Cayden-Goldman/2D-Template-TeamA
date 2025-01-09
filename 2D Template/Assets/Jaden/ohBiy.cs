using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ohBiy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new(Time.time * Random.Range(1f, 10), Time.time * Random.Range(1f, 10), Time.time * Random.Range(1f, 10));
    }
}
