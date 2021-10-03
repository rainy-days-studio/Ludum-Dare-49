using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct colourResult
{
    int pathLength, numOfIncoming;
}

public class ColourGraph : Manager<ColourGraph>
{
    // Array of nodes
    private ColourNode[] nodes;
    // Adjacenecy matrix of the nodes
    private bool[,] adjacencyMatrix;

    // Intialise variables
    public void init(string[] colourNames)
    {
        // Get names of colours
        string[] names = colourNames;

        // Initialises nodes and matrix
        nodes = new ColourNode[names.Length];
        adjacencyMatrix = new bool[nodes.Length, nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
            nodes[i] = new ColourNode(names[i], i);
    }

    // Create colour adjacencies
    private void createAdjacencies()
    {
        bool[,] emptyMatrix = new bool[nodes.Length, nodes.Length];
        Array.Copy(adjacencyMatrix, emptyMatrix, nodes.Length * nodes.Length);

    }



    // Get adjacency matrix of colours
    public bool[,] getAdjacencyMatrix()
    {
        return adjacencyMatrix;
    }
}
