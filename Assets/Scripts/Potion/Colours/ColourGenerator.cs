using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColourGenerator : Manager<ColourGenerator>
{
    // Created colours and interactions
    private List<ColourChange> interactions;
    private Dictionary<string, Colour> colours;

    // Colour graph instance
    private ColourGraph colourGraph;

    // Intialise variables
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        colourGraph = ColourGraph.Instance;

        generateColours();
        generateInteractions();
        IngredientGenerator.Instance.init();
    }
    
    // Generate all standard colours
    private void generateColours()
    {
        colours = new Dictionary<string, Colour>();
        colours.Add("Blue", new Colour("Blue", Color.blue));
        colours.Add("Red", new Colour("Red", Color.red));
        colours.Add("Green", new Colour("Green", Color.green));
        colours.Add("Purple", new Colour("Purple", new Color(0.625f, 0, 0.625f)));
        colours.Add("Grey", new Colour("Grey", Color.grey));
        colours.Add("Black", new Colour("Black", Color.black));
        colours.Add("Orange", new Colour("Orange", new Color(1, 0.64f, 0)));
        colours.Add("Yellow", new Colour("Yellow", Color.yellow));
        colours.Add("White", new Colour("White", Color.white));
        colours.Add("Pink",  new Colour("Pink", new Color(0.96f, 0.375f, 0.98f)));
    }

    // Generate all interactions
    private void generateInteractions()
    {
        string[] colourNames = colours.Keys.ToArray();

        colourGraph.init(colourNames);

        interactions = new List<ColourChange>();

        // Iterate through adjacency matrix and use it to construct colour interactions
        bool[,] adjacencyMatrix = colourGraph.getAdjacencyMatrix();

        List<string> availableColours = new List<string>(colourNames);

        for (int i = 0; i < colours.Count; i++)
        {
            for (int l = 0; l < colours.Count; l++)
            {
                if(adjacencyMatrix[i,l])
                {
                    string input1 = colourNames[i];
                    string output = colourNames[l];
                    string input2 = "";

                    while(true) 
                    { 
                        if (availableColours.Count == 0)
                            availableColours.AddRange(colourNames);

                        input2 = availableColours[Random.Range(0, availableColours.Count)];
                        if (!input2.Equals(input1) && !input2.Equals(output))
                        {
                            availableColours.Remove(input2);
                            break;
                        }
                        else if (availableColours.Count == 2)
                        {
                            input2 = availableColours[0];
                            if (input2.Equals(input1) || input2.Equals(output))
                            {
                                input2 = availableColours[1];
                                if (input2.Equals(input1) || input2.Equals(output))
                                    availableColours.AddRange(colourNames);
                                else
                                {
                                    availableColours.Remove(input2);
                                    break;
                                }
                            }
                            else
                            {
                                availableColours.Remove(input2);
                                break;
                            }
                        }
                        else if (availableColours.Count == 1)
                            availableColours.AddRange(colourNames);

                    }

                    interactions.Add(new ColourChange() { input1 = colours[input1], input2 = colours[input2], output = colours[output] });
                } 
            }
        }
    }

    public Dictionary<string, Colour> getColours()
    {
        return colours;
    }

    public List<ColourChange> getInteractions()
    {
        return interactions;
    }

    // Get a random colour
    public Colour getRandomColour()
    {
        string[] colourNames = colours.Keys.ToArray();
        return colours[colourNames[Random.Range(0, colourNames.Length)]];
    }
}
