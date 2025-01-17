using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Vessel : MonoBehaviour
{
    public static readonly Vector2Int[] directions = new Vector2Int[] { new(0, -1), new(-1, 0), new(0, 1), new(1, 0), new() };

    public static Vector2Int pos;
    public static bool ghostMode;
    public static bool canMove = true;
    public static bool paused;

    public GameObject ghostObj;
    public GameObject interactText;
    public Material possessMat;
    public Material defaultMat;

    Tilemap walls;
    bool moving;
    Sprite currentSprite;
    SpriteRenderer sr;
    Animator animator;
    int directionDown = -1;
    bool updatingShader;
    float ghostTimer;
    bool interactPending;
    bool timeOut;

    void Awake()
    {
        Movable.positions = new();
        Movable.objects = new();
        Interactables.positions = new();
        Interactables.interactables = new();
    }

    void Start()
    {
        ghostMode = paused = false;
        canMove = true;
        pos = new((int)transform.position.x, (int)transform.position.y);
        walls = GameObject.Find("Collidables").GetComponent<Tilemap>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name == "guh")
        {
            ghostMode = true;
            sr.material = defaultMat;
            Instantiate(ghostObj, new Vector3(0, -3), new());
            StartCoroutine(Sleeping());
        }
    }

    void Update()
    {
        if (!paused)
        {
            if (!moving && !ghostMode && canMove)
            {
                if (Input.GetKey(KeyCode.S))
                    directionDown = 0;
                else if (Input.GetKey(KeyCode.A))
                    directionDown = 1;
                else if (Input.GetKey(KeyCode.W))
                    directionDown = 2;
                else if (Input.GetKey(KeyCode.D))
                    directionDown = 3;
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    ghostMode = true;
                    sr.material = defaultMat;
                    Instantiate(ghostObj, new Vector3(pos.x, pos.y), new());
                    StartCoroutine(GhostTimer());
                    StartCoroutine(Sleeping());
                }
                else
                    animator.SetBool("IsWalking", false);
                if (directionDown > -1)
                {
                    StartCoroutine(Move(directions[directionDown]));
                    animator.SetInteger("Direction", directionDown);
                    directionDown = -1;
                }
                if (!updatingShader)
                    SetShader();
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
            else
                animator.SetBool("IsWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                paused = true;
                UiManager.pause.Invoke();
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                UiManager.pause.Invoke();
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator Sleeping()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        animator.SetBool("IsPossessed", false);
        yield return new WaitUntil(() => !ghostMode || timeOut);
        animator.SetBool("IsPossessed", true);
        GetComponentInChildren<ParticleSystem>().Stop();
    }

    IEnumerator Move(Vector2Int delta)
    {
        TileBase tile = walls.GetTile((Vector3Int)(pos + delta));
        if (tile == null)
        {
            if (Movable.positions.Contains(pos + delta))
            {
                tile = walls.GetTile((Vector3Int)(pos + delta * 2));
                if (tile == null)
                {
                    GameObject obj = Movable.objects[Movable.positions.IndexOf(pos + delta)];
                    Movable.positions[Movable.positions.IndexOf(pos + delta)] += delta;
                    moving = true;
                    animator.SetBool("IsWalking", true);
                    Vector2Int startPos = pos;
                    pos += delta;
                    for (float t = 0; t < 1; t += Time.deltaTime * 6)
                    {
                        transform.position = Vector2.Lerp(startPos, pos, t);
                        obj.transform.position = transform.position + (Vector3)(Vector2)delta;
                        yield return null;
                    }
                    transform.position = (Vector2)pos;
                    obj.transform.position = transform.position + (Vector3)(Vector2)delta;
                }
            }
            else if (!Interactables.positions.Contains(pos + delta))
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
                transform.position = (Vector2)pos;
            }
            moving = false;
            if (!interactPending)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (Movable.positions.Contains(pos + directions[i]) && walls.GetTile((Vector3Int)(pos + directions[i] * 2)) != null && walls.GetTile((Vector3Int)(pos + directions[i] * -1)) == null)
                    {
                        GameObject movable = Movable.objects[Movable.positions.IndexOf(pos + directions[i])];
                        SetText("Pull", new Vector2(0.5f, 1) + directions[i]);
                        interactPending = true;
                        while (!moving)
                        {
                            yield return null;
                            if (Input.GetKeyDown(KeyCode.E) && canMove)
                            {
                                delta = directions[i] * -1;
                                Movable.positions[Movable.positions.IndexOf(pos + directions[i])] += delta;
                                moving = true;
                                animator.SetInteger("Direction", (i + 2) % 4);
                                animator.SetBool("IsWalking", true);
                                Vector2Int startPos = pos;
                                pos += delta;
                                for (float t = 0; t < 1; t += Time.deltaTime * 6)
                                {
                                    transform.position = Vector2.Lerp(startPos, pos, t);
                                    movable.transform.position = transform.position - (Vector3)(Vector2)delta;
                                    yield return null;
                                }
                                transform.position = (Vector2)pos;
                                movable.transform.position = transform.position - (Vector3)(Vector2)delta;
                                moving = false;
                                break;
                            }
                        }
                        interactPending = false;
                        interactText.SetActive(false);
                        break;
                    }
                    else if (Interactables.positions.Contains(pos + directions[i]))
                    {
                        Interactable interactable = Interactables.interactables[Interactables.positions.IndexOf(pos + directions[i])];
                        if (!interactable.ghostOnly)
                        {
                            SetText(interactable.text, new Vector2(0.5f, 1) + directions[i]);
                            interactPending = true;
                            while (!moving)
                            {
                                yield return null;
                                if (Input.GetKeyDown(KeyCode.E) && canMove)
                                    interactable.Interact();
                            }
                            interactPending = false;
                            interactText.SetActive(false);
                            break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator UpdateShader()
    {
        updatingShader = true;
        while (true)
        {
            yield return new WaitUntil(() => currentSprite != sr.sprite);
            if (!ghostMode)
            {
                currentSprite = sr.sprite;
                sr.material.SetTexture("_Texture", currentSprite.texture);
            }
            else break;
        }
        updatingShader = false;
    }

    public void SetShader()
    {
        sr.material = possessMat;
        StartCoroutine(UpdateShader());
    }

    void SetText(string text, Vector2 pos)
    {
        interactText.GetComponent<TextMeshPro>().text = text;
        foreach (TextMeshPro outline in interactText.GetComponentsInChildren<TextMeshPro>())
            outline.text = text;
        interactText.transform.localPosition = pos;
        interactText.SetActive(true);
    }

    IEnumerator GhostTimer()
    {
        TextMeshPro text = interactText.GetComponent<TextMeshPro>();
        ghostTimer = 20;
        while (ghostMode)
        {
            if (ghostTimer > 0 && !paused)
            {
                ghostTimer -= Time.deltaTime;
                SetText(Mathf.CeilToInt(ghostTimer) + "", new(0.5f, 1));
                if (ghostTimer < 5)
                    text.color = Color.red + Color.cyan * Mathf.CeilToInt(Mathf.Sin(Time.time * 10));
            }
            else if (ghostTimer <= 0)
            {
                UiManager.failDetails = "Your vessel woke up";
                UiManager.fail.Invoke();
                timeOut = true;
                Ghost.canMove = false;
                break;
            }
            yield return null;
        }
        text.color = Color.white;
        interactText.SetActive(false);
    }
}
