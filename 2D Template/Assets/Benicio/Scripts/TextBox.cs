using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextBox : MonoBehaviour
{
    public static UnityEvent writeText = new();
    public static Queue names = new();
    public static Queue texts = new();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI text;

    RectTransform rect;
    bool writing;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        writeText.AddListener(StartText);
    }

    void StartText()
    {
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        yield return new WaitUntil(() => !writing);
        text.text = "";
        nameText.text = (string)names.Dequeue();
        Vessel.canMove = false;
        Ghost.canMove = false;
        Vessel.paused = true;
        writing = true;
        for (float t = 0; t < Mathf.PI / 2f; t += Time.deltaTime * 1.5f)
        {
            rect.anchoredPosition = Vector3.Lerp(new(0, -720), new(0, -288), Mathf.Pow(Mathf.Sin(t), 2));
            yield return null;
        }
        while (texts.Count > 0)
        {
            text.text = "";
            string nextText = (string)texts.Dequeue();
            StartCoroutine(AudioManager.PlaySound("Textbox", 0));
            for (int i = 0; i < nextText.Length; i++) 
            {
                yield return new WaitForSeconds(0.02f);
                text.text += nextText[i];
            }
            AudioManager.loopChannels[0] = 2;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        Vessel.canMove = true;
        Ghost.canMove = true;
        Vessel.paused = false;
        for (float t = 0; t < Mathf.PI / 2f; t += Time.deltaTime * 1.5f)
        {
            rect.anchoredPosition = Vector3.Lerp(new(0, -288), new(0, -720), Mathf.Pow(Mathf.Sin(t), 2));
            yield return null;
        }
        rect.anchoredPosition = new(0, -720);
        writing = false;
    }

    public static void AddDialogue(string name, string text, bool start = false)
    {
        names.Enqueue(name);
        texts.Enqueue(text);
        if (start)
            writeText.Invoke();
    }
}
