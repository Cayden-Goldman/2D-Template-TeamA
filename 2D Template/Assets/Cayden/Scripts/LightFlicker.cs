using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    Light2D light;
    float lightness = 0.9f;
    float green = 0.3f;

    void Start()
    {
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        lightness = Mathf.Clamp(lightness + Time.deltaTime * 3 * Random.Range(-1, 1f), 0.9f, 1);
        light.pointLightOuterRadius = lightness * 3;
        green = Mathf.Clamp(green + Time.deltaTime * 2 * Random.Range(-1, 1f), 0.2f, 0.35f);
        light.color = new Color(1, green, 0) * lightness;
    }
}
