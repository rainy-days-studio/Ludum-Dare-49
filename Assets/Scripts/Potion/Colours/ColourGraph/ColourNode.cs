using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourNode
{
    // Name of colour the node represents
    private string name;
    // Index of node
    private int index;

    public ColourNode(string name, int index)
    {
        this.name = name;
        this.index = index;
    }
}
