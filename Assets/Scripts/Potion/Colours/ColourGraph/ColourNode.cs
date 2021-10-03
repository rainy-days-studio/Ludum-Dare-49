using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourNode
{
    // Name of colour the node represents
    private string name;
    // Index of node
    private int index;

    // Number of incoming  nodes
    private int incoming;

    public ColourNode(string name, int index)
    {
        this.name = name;
        this.index = index;
        incoming = 0;
    }

    public void setIncoming(int incoming)
    {
        this.incoming = incoming;
    }

    public string getName()
    {
        return name;
    }

    public int getIndex()
    {
        return index;
    }

    public int getIncoming()
    {
        return incoming;
    }
}
