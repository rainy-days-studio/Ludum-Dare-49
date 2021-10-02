using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HighlightObj : MonoBehaviour
{
    SpriteRenderer Renderer;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
    }

    private void OnMouseEnter()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0.1f);
    }

    private void OnMouseExit()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
    }



}
