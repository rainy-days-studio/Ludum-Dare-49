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
    // Canvas group of object
    private CanvasGroup canvasGroup;
    // Physics manager
    private PhysicsManager physics;
    // Rigidbody
    private Rigidbody2D rigidBody;
    // Highlight obj
    private HighlightObj highlightObj;
    // Colliding with table
    [SerializeField]
    private bool touchingTableDefault;
    private bool touchingTable;
    // Being dragged
    private bool dragging;
    // Whether to reset the object
    private bool resetStatus;
    // Starting position of the object
    Vector2 startingPosition;
    // Starting gravity scale
    private float startingGravity;

    // Initialise variables
    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;
        highlightObj = GetComponent<HighlightObj>();
        touchingTable = touchingTableDefault;
        dragging = false;
        resetStatus = false;
        startingPosition = transform.anchoredPosition;
        startingGravity = rigidBody.gravityScale;
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
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        canvasGroup.blocksRaycasts = true;
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

            if (resetStatus)
                reset();
        }
    }

    // Reset object to starting state
    public void setReset()
    {
        resetStatus = true;
        transform.anchoredPosition = startingPosition;
    }

    private void reset()
    {
        rigidBody.gravityScale = startingGravity;
        rigidBody.velocity = Vector2.zero;
        dragging = false;
        touchingTable = touchingTableDefault;
        resetStatus = false;
        if (highlightObj)
            highlightObj.reset();
    }
}
