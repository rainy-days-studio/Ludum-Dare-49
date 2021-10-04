using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientObject : MonoBehaviour
{
    // Ingredient this object uses
    private Ingredient ingredient;
    // Image of the object
    private Image image;
    // Drag and drop of this object
    private DragAndDrop dragAndDrop;
    // Text that floats above the ingredient
    private Text text;

    // Initialise variables
    void Awake()
    {
        image = GetComponent<Image>();
        dragAndDrop = GetComponent<DragAndDrop>();
        text = GetComponentInChildren<Text>();
        text.enabled = false;
        gameObject.SetActive(false);
    }

    // Initialise state
    public void init(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        image.sprite = ingredient.getSprite();
        text.text = ingredient.getName();
        gameObject.SetActive(true);
    }

    // Put this ingredient in the potion consuming it
    public Ingredient consume()
    {
        dragAndDrop.setReset();
        return ingredient;
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
