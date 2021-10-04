using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientObject : MonoBehaviour
{
    // Ingredient this object uses
    private Ingredient ingredient;
    private Image image;

    // Initialise variables
    void Awake()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    // Initialise state
    public void init(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        image.sprite = ingredient.getSprite();
        gameObject.SetActive(true);
    }

    // Put this ingredient in the potion consuming it
    public Ingredient consume()
    {
        gameObject.SetActive(false);
        return ingredient;
    }
}
