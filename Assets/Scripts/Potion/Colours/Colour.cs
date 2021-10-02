using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColourChange
{
    public Colour input1, input2, output;
}

public class Colour
{
    private string name;
    private Color unityColour;

    public Colour(string name, Color unityColour)
    {
        this.name = name;
        this.unityColour = unityColour;
    }

    public string getName()
    {
        return name;
    }

    public Color getUnityColour()
    {
        return unityColour;
    }

    // Overload plus operator so it can be used to combine colours
    public static Colour operator +(Colour a, Colour b)
    {
        ColourChange[] interactions = ColourGenerator.Instance.getInteractions().ToArray();

        for (int i = 0; i < interactions.Length; i++)
            if (interactions[i].input1 == a)
                if (interactions[i].input2 == b)
                    return interactions[i].output;

        return a;
    }
}
