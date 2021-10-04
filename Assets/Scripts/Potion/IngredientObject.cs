using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    // Ingredient this object uses
    private Ingredient ingredient;
    private SpriteRenderer sprite;

    // Initialise variables
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    // Initialise state
    public void init(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        sprite.sprite = ingredient.getSprite();
        gameObject.SetActive(true);
    }

    // Put this ingredient in the potion consuming it
    public Ingredient consume()
    {
        gameObject.SetActive(false);
        return ingredient;
    }
}
