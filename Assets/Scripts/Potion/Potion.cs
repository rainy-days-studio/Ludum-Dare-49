using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion
{
    // Current colour of the potion
    private Color colour;
    // Current fizziness of the potion
    private PotionFizziness fizziness;

    // Number of ingredients
    private uint ingredients;

    // Add an ingredient returns true if potion explodes
    public bool addIngredient(Ingredient ingredient)
    {
        colour += ingredient.getPotionEffect(fizziness);
        ingredients++;
        return false;
    }
}
