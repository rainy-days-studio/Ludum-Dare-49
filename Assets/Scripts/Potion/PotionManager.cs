using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        maxIngredients = Mathf.RoundToInt((check.pathLength * 4) / (check.numOfIncoming / 3));
    }

    // Check if the potion is correct or over the max
    public void checkPotion(Colour colour, int ingredients)
    {
        if (targetColour == colour || ingredients >= maxIngredients)
        {
            activePotion.finish();
            activatePotion();
        }
    }
}
