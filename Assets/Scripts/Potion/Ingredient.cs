using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient
{
    // Ingredient properties
    private string name;
    private Sprite sprite;

    // How ingredient interacts with potion
    private Colour colour;

    public Ingredient(string name, Sprite sprite, Colour colour)
    {
        this.name = name;
        this.sprite = sprite;
        this.colour = colour;
    }

    public Colour getPotionEffect()
    {
        Colour outputColour = colour;

        return colour;
    }
}
