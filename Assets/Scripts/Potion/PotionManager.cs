using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : Manager<PotionManager>
{
    // Potions to use
    [SerializeField]
    private Potion[] potions;
    private Potion activePotion;
    private Colour targetColour;
    private ColourGenerator colourGenerator;
    // Max number of ingredients
    private int maxIngredients;
    // Text for board
    [SerializeField]
    private Text maxIngredientsText;
    [SerializeField]
    private Text usedIngredientsText;

    // Intialise variables
    void Start()
    {
        colourGenerator = ColourGenerator.Instance;
    }

    // Activate a potion for use
    public void activatePotion()
    {
        Colour potionColour = colourGenerator.getRandomColour();


        while (true)
        {
            targetColour = colourGenerator.getRandomColour();
            if (targetColour != potionColour)
                break;
        }

        activePotion = potions[Random.Range(0, potions.Length)];
        activePotion.init(potionColour);

        ColourResult check = ColourGraph.Instance.checkColourPath(potionColour.getName(), targetColour.getName());

        maxIngredients = Mathf.RoundToInt(((float) check.pathLength * 4) / (((float)check.numOfIncoming) / 3));

        maxIngredientsText.text = maxIngredients.ToString();
        usedIngredientsText.text = "0";
    }

    // Check if the potion is correct or over the max
    public void checkPotion(Colour colour, int ingredients)
    {
        usedIngredientsText.text = ingredients.ToString();
        if (targetColour == colour)
            activePotion.finish();
        if (ingredients >= maxIngredients)
            activePotion.stopInteracting();
    }
}
