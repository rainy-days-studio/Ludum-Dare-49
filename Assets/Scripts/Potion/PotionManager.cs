using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : Manager<PotionManager>
{
    // Potions to use
    [SerializeField]
    private Potion[] potions;
    private Potion activePotion;
    private ColourGenerator colourGenerator;

    // Intialise variables
    void Start()
    {
        colourGenerator = ColourGenerator.Instance;
    }

    // Activate a potion for use
    public void activatePotion()
    {
        activePotion = potions[Random.Range(0, potions.Length)];
        activePotion.init(colourGenerator.getRandomColour());
    }
}
