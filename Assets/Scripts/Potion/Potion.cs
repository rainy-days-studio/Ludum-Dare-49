using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Potion : MonoBehaviour, IDropHandler
{
    // Images of potion
    [SerializeField]
    private Image liquid;
    private Image bottle;
    private Sprite defaultBottle;
    private Sprite defaultLiquid;
    // Current colour of the potion
    private Colour colour;
    // Number of ingredients
    private int ingredients;
    // Drag and drop component
    private DragAndDrop dragAndDrop;
    // Whether the object can be interacted with
    private bool interactable;
    // Animation to play when smashing
    private Animator animator;
    // Text for colour
    private Text text;

    // Set variables
    private void Awake()
    {
        bottle = GetComponent<Image>();
        defaultBottle = bottle.sprite;
        defaultLiquid = liquid.sprite;
        dragAndDrop = GetComponent<DragAndDrop>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
        text = GetComponentInChildren<Text>();
        text.enabled = false;
        interactable = false;
        dragAndDrop.enabled = false;
        gameObject.SetActive(false);
    }

    // Initialise state of the potion
    public void init(Colour colour)
    {
        this.colour = colour;
        ingredients = 0;

        liquid.color = colour.getUnityColour();

        text.text = colour.getName();

        interactable = true;

        gameObject.SetActive(true);
        dragAndDrop.enabled = true;
    }

    // Add an ingredient returns true if potion explodes
    private bool addIngredient(Ingredient ingredient)
    {
        colour += ingredient.getPotionEffect();
        ingredients++;

        liquid.color = colour.getUnityColour();

        text.text = colour.getName();

        PotionManager.Instance.checkPotion(colour, ingredients);
        AudioManager.Instance.Play("Bubbles_3");
        return false;
    }

    // Set object to no longer interact
    private void stopInteraction()
    {
        interactable = false;
        dragAndDrop.enabled = false;
    }

    // When the potion is correct
    public void win()
    {
        stopInteraction();
        AudioManager.Instance.Play("Win");
        StartCoroutine(WinWait());
    }

    private IEnumerator WinWait()
    {
        yield return new WaitForSeconds(2);
        finish();
    }

    // When the potion is destroyed
    public void lose()
    {
        stopInteraction();
        animator.enabled = true;
    }

    // When potion is finished deactivate
    public void finish()
    {
        animator.enabled = false;
        dragAndDrop.setReset();
        gameObject.SetActive(false);
        bottle.sprite = defaultBottle;
        liquid.sprite = defaultLiquid;
        PotionManager.Instance.activatePotion();
    }

    // Mix ingredient if other object is an ingredient
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag.Equals("Ingredient") && interactable)
            addIngredient(eventData.pointerDrag.GetComponent<IngredientObject>().consume());
    }

    // Make text appear depending on whether the mouse is over
    private void OnMouseEnter()
    {
        text.enabled = true;
    }

    private void OnMouseExit()
    {
        text.enabled = false;
    }
}
