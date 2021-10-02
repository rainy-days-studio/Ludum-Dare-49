using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : Manager<IngredientGenerator>
{
    // Number of ingredients to generate
    [SerializeField]
    private uint ingredientNum;
    // Sprites to be used for ingredients
    [SerializeField]
    private List<Sprite> sprites;
    // Text file containing names of ingredients
    [SerializeField]
    private TextAsset nameFile;
    // List of names to use for ingredients
    private List<string> names;
    // List of ingredients that have been generated
    private List<Ingredient> ingredients;

    // On script awake read text
    public void Start()
    {
        getNamesFromText();
    }

    // Initialise variables
    public void init()
    {
        generateIngredients();
    }

    // Get names from text file
    private void getNamesFromText()
    {
        string text = nameFile.text;
        names = new List<string>(text.Split('\n'));
    }

    // Generate the ingredients
    private void generateIngredients()
    {
        ingredients = new List<Ingredient>();

        foreach (Colour colour in ColourGenerator.Instance.getColours())
        {
            // Random indexes
            int nameIndex = Random.Range(0, names.Count);
            int spriteIndex = Random.Range(0, sprites.Count);
            ingredients.Add(new Ingredient(names[nameIndex], sprites[spriteIndex], colour));
            names.RemoveAt(nameIndex);
            sprites.RemoveAt(spriteIndex);
        }
    }

    // Get generated ingredients
    public Ingredient[] GetIngredients() 
    {
        return ingredients.ToArray();
    }
}
