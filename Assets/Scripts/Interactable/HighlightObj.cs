using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HighlightObj : MonoBehaviour
{
    public bool bounce;
    public float bounceFactor;
    bool held;

    SpriteRenderer Renderer;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
    }

    private void Update()
    {
        if (held)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    private void OnMouseEnter()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0.1f);
        if (bounce)
            Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), bounceFactor);

    }

    private void OnMouseExit()
    {
        Renderer.material.SetFloat(Shader.PropertyToID("_OutlineThickness"), 0f);
        if (bounce)
            Renderer.material.SetFloat(Shader.PropertyToID("_BounceFactor"), 0f);
    }

    private void OnMouseDown()
    {
        held = true;
    }

    private void OnMouseUp()
    {
        held = false;
    }
}
