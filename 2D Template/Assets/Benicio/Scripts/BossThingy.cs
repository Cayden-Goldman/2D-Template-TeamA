using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossThingy : MonoBehaviour
{
    public bool onBoss;
    public GameObject ghost;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        ghost = collision.gameObject;
        if (collision.gameObject.CompareTag("Ghost"))
            onBoss = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        onBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onBoss && Input.GetKeyDown(KeyCode.Space))
        {
            ghost.SetActive(false);
            SceneManager.LoadScene("Title Scene");
        }
    }
}
