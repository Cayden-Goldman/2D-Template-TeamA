using UnityEngine;
public class TextTrigger : MonoBehaviour
{
    public string text;
    public string npcName;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        TextBox.AddDialogue(npcName, text, true);
        gameObject.SetActive(false);
    }
}
