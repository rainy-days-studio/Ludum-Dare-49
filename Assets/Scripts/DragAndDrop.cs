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
    // Colliding with table
    private bool touchingTable;
    // Being dragged
    private bool dragging;

    // Initialise variables
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        touchingTable = true;
        dragging = false;
    }

    private void Start()
    {
        physics = PhysicsManager.Instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = transform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        position.x = Mathf.Clamp(position.x, -physics.checkHorizontalOffset(), physics.checkHorizontalOffset());
        position.y = Mathf.Clamp(position.y, -physics.checkVerticalOffset(), physics.checkVerticalOffset());

        transform.anchoredPosition = (position / canvas.scaleFactor);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        if (!touchingTable)
            rigidBody.gravityScale = physics.getGravity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Table"))
        {
            touchingTable = true;
            rigidBody.gravityScale = 0;
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Table"))
        {
            touchingTable = false;
            if (!dragging)
                rigidBody.gravityScale = physics.getGravity();
        }
    }
}
