using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public static Vector2Int pos;
    public static bool canMove;

    public static readonly List<string> passableTiles = new() { "baul", "CrateInner" };

    public GameObject interactText;

    Tilemap walls;
    Tilemap guardTiles;
    bool moving;
    Sprite currentSprite;
    SpriteRenderer sr;
    Animator animator;
    int directionDown = -1;

    void Start()
    {
        canMove = true;
        pos = new((int)transform.position.x, (int)transform.position.y);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        guardTiles = GameObject.Find("DangerTiles").GetComponent<Tilemap>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(UpdateShader());
    }

    void Update()
    {
        if (!Vessel.paused)
        {
            if (!moving && canMove)
            {
                if (Input.GetKey(KeyCode.S))
                    directionDown = 0;
                else if (Input.GetKey(KeyCode.A))
                    directionDown = 1;
                else if (Input.GetKey(KeyCode.W))
                    directionDown = 2;
                else if (Input.GetKey(KeyCode.D))
                    directionDown = 3;
                else if (Input.GetKeyDown(KeyCode.Space) && pos == Vessel.pos)
                {
                    StartCoroutine(AudioManager.PlaySound("Possess"));
                    Vessel.ghostMode = false;
                    Destroy(gameObject);
                }
                else
                    animator.SetBool("IsWalking", false);
                if (directionDown > -1)
                {
                    StartCoroutine(Move(Vessel.directions[directionDown]));
                    animator.SetInteger("Direction", directionDown);
                    directionDown = -1;
                }
            }
            else if (moving)
            {
                if (Input.GetKeyDown(KeyCode.S))
                    directionDown = 0;
                else if (Input.GetKeyDown(KeyCode.A))
                    directionDown = 1;
                else if (Input.GetKeyDown(KeyCode.W))
                    directionDown = 2;
                else if (Input.GetKeyDown(KeyCode.D))
                    directionDown = 3;
            }
        }
    }

    IEnumerator Move(Vector2Int delta)
    {
        TileBase tile = walls.GetTile((Vector3Int)(pos + delta));
        bool canMove = false;
        if (tile == null) 
            canMove = true;
        else if (passableTiles.Contains(tile.name))
            canMove = true;
        if (canMove)
        {
            moving = true;
            animator.SetBool("IsWalking", true);
            Vector2Int startPos = pos;
            pos += delta;
            for (float t = 0; t < 1; t += Time.deltaTime * 6)
            {
                transform.position = Vector2.Lerp(startPos, pos, t);
                yield return null;
            }
            moving = false;
            if (guardTiles.GetTile((Vector3Int)pos) != null)
            {
                UiManager.failDetails = "You were caught by a guard";
                UiManager.fail.Invoke();
                Npc.playerFound.Invoke();
                Vessel.canMove = false;
                Ghost.canMove = false;
            }
            for (int i = 0; i < 5; i++)
            {
                if (Interactables.positions.Contains(pos + Vessel.directions[i]))
                {
                    Interactable interactable = Interactables.interactables[Interactables.positions.IndexOf(pos + Vessel.directions[i])];
                    if (!interactable.vesselOnly)
                    {
                        interactText.GetComponent<TextMeshPro>().text = interactable.text;
                        foreach (TextMeshPro outline in interactText.GetComponentsInChildren<TextMeshPro>())
                            outline.text = interactable.text;
                        interactText.transform.localPosition = new Vector2(0.5f, 1) + Vessel.directions[i];
                        interactText.SetActive(true);
                        while (!moving)
                        {
                            yield return null;
                            if (Input.GetKeyDown(KeyCode.E) && canMove)
                            {
                                interactable.Interact();
                            }
                        }
                        interactText.SetActive(false);
                        break;
                    }
                }
            }
        }
    }

    IEnumerator UpdateShader()
    {
        while (true)
        {
            yield return new WaitUntil(() => currentSprite != sr.sprite);
            currentSprite = sr.sprite;
            sr.material.SetTexture("_Texture", currentSprite.texture);
        }
    }
}
