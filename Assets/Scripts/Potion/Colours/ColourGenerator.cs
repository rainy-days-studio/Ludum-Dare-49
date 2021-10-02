using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourGenerator : Manager<ColourGenerator>
{
    // Created colours and interactions
    List<Colour> colours;
    List<ColourChange> interactions;

    private void Start()
    {
        colours = new List<Colour>();
        interactions = new List<ColourChange>();

        colours.Add(new Colour("Blue", Color.blue));
        colours.Add(new Colour("Red", Color.red));
        colours.Add(new Colour("Green", Color.green));
        colours.Add(new Colour("Purple", new Color(160, 0, 160)));
        colours.Add(new Colour("Grey", Color.grey));
        colours.Add(new Colour("Black", Color.black));
        colours.Add(new Colour("Orange", new Color(160, 130, 0)));
        colours.Add(new Colour("Yellow", Color.yellow));
        colours.Add(new Colour("White", Color.white));

        interactions.Add(new ColourChange() { input1 = colours[0], input2 = colours[6], output = colours[1] });
        interactions.Add(new ColourChange() { input1 = colours[1], input2 = colours[3], output = colours[2] });
        interactions.Add(new ColourChange() { input1 = colours[2], input2 = colours[8], output = colours[3] });
        interactions.Add(new ColourChange() { input1 = colours[3], input2 = colours[7], output = colours[4] });
        interactions.Add(new ColourChange() { input1 = colours[4], input2 = colours[6], output = colours[5] });
        interactions.Add(new ColourChange() { input1 = colours[7], input2 = colours[8], output = colours[0] });
        interactions.Add(new ColourChange() { input1 = colours[7], input2 = colours[1], output = colours[8] });
        interactions.Add(new ColourChange() { input1 = colours[8], input2 = colours[3], output = colours[6] });
        interactions.Add(new ColourChange() { input1 = colours[6], input2 = colours[2], output = colours[1] });
        interactions.Add(new ColourChange() { input1 = colours[6], input2 = colours[4], output = colours[7] });
        interactions.Add(new ColourChange() { input1 = colours[6], input2 = colours[8], output = colours[3] });
    }

    public List<Colour> getColours()
    {
        return colours;
    }

    public List<ColourChange> getInteractions()
    {
        return interactions;
    }
}
