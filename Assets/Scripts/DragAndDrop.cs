using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Transform of object
    private new RectTransform transform;
    // Canvas this object is rendered on
    private Canvas canvas;
    // Physics manager
    private PhysicsManager physics;
    // Rigidbody
    private Rigidbody2D rigidBody;

    // Initialise variables
    private void Start()
    {
        transform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        physics = PhysicsManager.Instance;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = physics.getGravity();
        rigidBody.freezeRotation = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rigidBody.gravityScale = physics.getGravity();
    }
}
