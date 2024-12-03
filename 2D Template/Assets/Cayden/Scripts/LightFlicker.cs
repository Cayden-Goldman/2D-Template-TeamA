using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    Light2D light;
    float lightness;
    float green = 0.3f;
    float startRad;

    void Start()
    {
        light = GetComponent<Light2D>();
        startRad = light.pointLightOuterRadius;
        lightness = startRad - 0.1f;
    }

    void Update()
    {
        lightness = Mathf.Clamp(lightness + Time.deltaTime * 3 * Random.Range(-1, 1f), startRad - 0.1f, startRad);
        light.pointLightOuterRadius = lightness;
        green = Mathf.Clamp(green + Time.deltaTime * 2 * Random.Range(-1, 1f), 0.2f, 0.35f);
        light.color = new Color(1, green, 0);
    }
}
