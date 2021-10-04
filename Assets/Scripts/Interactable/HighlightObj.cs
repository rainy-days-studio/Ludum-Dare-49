using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class HighlightObj : MonoBehaviour
{
    [SerializeField]
    private float outlineThickness = 0.1f;
    [SerializeField]
    private bool bounce;
    [SerializeField]
    private float bounceFactor;

    private Image Renderer;

    private void Awake()
    {
        Renderer = GetComponent<Image>();
        Material mat = Instantiate(Renderer.material);
        Renderer.material = mat;
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
        Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), bounceFactor);
    }

    private void OnMouseEnter()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), outlineThickness);
        /*
        if (bounce)
            Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), bounceFactor);
        */

    }

    private void OnMouseExit()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
        /*
        if (bounce)
            Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), 0f);
        */
    }

    // When clicked disable bouncing
    private void OnMouseDown()
    {
        if (bounce)
        {
            bounce = false;
            Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), 0);
        }
    }

    // When reset reactivate bouncing
    public void reset()
    {
        bounce = true;
        Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), bounceFactor);
    }
}
