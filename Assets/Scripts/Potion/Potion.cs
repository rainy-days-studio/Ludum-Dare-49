using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    // Image of potion liquid
    [SerializeField]
    private Image liquid;
    // Current colour of the potion
    private Colour colour;
    // Current fizziness of the potion
    private PotionFizziness fizziness;

    // Number of ingredients
    private int ingredients;

    // Set variables
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Initialise state of the potion
    public void init(Colour colour)
    {
        this.colour = colour;
        ingredients = 0;

        liquid.color = colour.getUnityColour();

        gameObject.SetActive(true);
    }

    // Add an ingredient returns true if potion explodes
    private bool addIngredient(Ingredient ingredient)
    {
        colour += ingredient.getPotionEffect();
        ingredients++;

        liquid.color = colour.getUnityColour();

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

    // Mix ingredient if other object is an ingredient
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Ingredient"))
            addIngredient(other.gameObject.GetComponent<IngredientObject>().consume());
    }
}
