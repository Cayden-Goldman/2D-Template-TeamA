using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ColorChanging : MonoBehaviour
{
    public UnityEngine.UI.Image Panel;
    public Color Blue;
    public Color Purple;
    public Color Gray;
    private int x;

    void Start()
    {
        Load();
    }

    public void ChangeColor(string color)
    {
        if(color == "Blue")
        {
            Panel.color = Blue;
            Save();
        }
        else if (color == "Purple")
        {
            Panel.color = Purple;
            Save();
        }
        else if (color == "Gray")
        {
            Panel.color = Gray;
            Save();
        }
    }
    private void Load()
    {
        float r = PlayerPrefs.GetFloat("Red");
        float g = PlayerPrefs.GetFloat("Green");
        float b = PlayerPrefs.GetFloat("Blue");
        Panel.color = new Color(r, g, b);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Red", Panel.color.r);
        PlayerPrefs.SetFloat("Green", Panel.color.g);
        PlayerPrefs.SetFloat("Blue", Panel.color.b);
    }
}
