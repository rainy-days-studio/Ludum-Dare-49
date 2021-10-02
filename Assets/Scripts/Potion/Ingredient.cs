using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    // Ingredient properties
    private string name;
    private Sprite sprite;

    // How ingredient interacts with potion
    private Color flatColour;
    private Color bubblingColour;

    public Ingredient(string name, Sprite sprite, Color flatColour, Color bubblingColour)
    {
        this.name = name;
        this.sprite = sprite;
        this.flatColour = flatColour;
        this.bubblingColour = bubblingColour;
    }

    public Color getPotionEffect(PotionFizziness potionFizziness)
    {
        Color colour = Color.clear;
        if (potionFizziness == PotionFizziness.flat)
            colour += flatColour;
        else
            colour += bubblingColour;

        return colour;
    }
}
