using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionManager : Manager<PotionManager>
{
    // Potions to use
    [SerializeField]
    private Potion[] potions;
    // Target Potions to use
    [SerializeField]
    private TargetPotion[] targetPotions;
    private Potion activePotion;
    private TargetPotion activeTargetPotion;
    private Colour targetColour;
    private ColourGenerator colourGenerator;
    // Max number of ingredients
    private int maxIngredients;
    // Text for board
    [SerializeField]
    private Text maxIngredientsText;
    [SerializeField]
    private Text usedIngredientsText;
    [SerializeField]
    private Text scoreText;
    // Score
    private int score;

    // Intialise variables
    public override void Awake()
    {
        base.Awake();
        score = 0;
    }

    private void Start()
    {
        colourGenerator = ColourGenerator.Instance;
    }

    // Activate a potion for use
    public void activatePotion()
    {
        ColourResult check;
        Colour potionColour;
        do
        {
            potionColour = colourGenerator.getRandomColour();

            while (true)
            {
                targetColour = colourGenerator.getRandomColour();
                if (targetColour != potionColour)
                    break;
            }

            check = ColourGraph.Instance.checkColourPath(potionColour.getName(), targetColour.getName());
        } while (check.pathLength == -1);

        int index = Random.Range(0, potions.Length);

        activePotion = potions[index];
        activePotion.init(potionColour);

        activeTargetPotion = targetPotions[index];
        activeTargetPotion.setTargetColour(targetColour);


        maxIngredients = Mathf.RoundToInt(((float) check.pathLength * 4) / (((float)check.numOfIncoming) / 2));

        maxIngredientsText.text = maxIngredients.ToString();
        usedIngredientsText.text = "0";
    }

    // Check if the potion is correct or over the max
    public void checkPotion(Colour colour, int ingredients)
    {
        usedIngredientsText.text = ingredients.ToString();
        if (targetColour == colour)
        {
            activePotion.win();
            score++;
            scoreText.text = score.ToString();
            activeTargetPotion.disable();
        }
        else if (ingredients >= maxIngredients)
        {
            activePotion.lose();
            score--;
            scoreText.text = score.ToString();
            AudioManager.Instance.Play("Shatter");
            activeTargetPotion.disable();
        }
    }
}
