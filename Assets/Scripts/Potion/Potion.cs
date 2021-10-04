using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Potion : MonoBehaviour, IDropHandler
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
    // Drag and drop component
    private DragAndDrop dragAndDrop;

    // Set variables
    private void Awake()
    {
        dragAndDrop = GetComponent<DragAndDrop>();
        gameObject.SetActive(false);
    }

    // Initialise state of the potion
    public void init(Colour colour)
    {
        this.colour = colour;
        ingredients = 0;

        liquid.color = colour.getUnityColour();

        dragAndDrop.setReset();

        gameObject.SetActive(true);
    }

    // Add an ingredient returns true if potion explodes
    private bool addIngredient(Ingredient ingredient)
    {
        colour += ingredient.getPotionEffect();
        ingredients++;

        liquid.color = colour.getUnityColour();

        PotionManager.Instance.checkPotion(colour, ingredients);

        return false;
    }

    // When potion is finished deactivate
    public void finish()
    {

        gameObject.SetActive(false);
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
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag.Equals("Ingredient"))
            addIngredient(eventData.pointerDrag.GetComponent<IngredientObject>().consume());
    }
}
