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
        colours.Add("Blue", new Colour(Color.blue));
        colours.Add("Red", new Colour(Color.red));
        colours.Add("Green", new Colour(Color.green));
        colours.Add("Purple", new Colour(new Color(160, 0, 160)));
        colours.Add("Grey", new Colour(Color.grey));
        colours.Add("Black", new Colour(Color.black));
        colours.Add("Orange", new Colour(new Color(160, 130, 0)));
        colours.Add("Yellow", new Colour(Color.yellow));
        colours.Add("White", new Colour(Color.white));
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
}
