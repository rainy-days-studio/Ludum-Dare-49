using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion
{
    // Current colour of the potion
    private Colour colour;
    // Current fizziness of the potion
    private PotionFizziness fizziness;

    // Number of ingredients
    private uint ingredients;

    // Add an ingredient returns true if potion explodes
    public bool addIngredient(Ingredient ingredient)
    {
        colour += ingredient.getPotionEffect();
        ingredients++;
        return false;
    }

    // Stir the potion
    public void stir()
    {
        if (fizziness == PotionFizziness.bubbling)
            fizziness = PotionFizziness.flat;
    }
    
    // Shake the potion
    public void shake()
    {
        if (fizziness == PotionFizziness.flat)
            fizziness = PotionFizziness.bubbling;
    }
}
