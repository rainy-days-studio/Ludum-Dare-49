using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public struct ColourResult
{
    public int pathLength, numOfIncoming;
}

public class ColourGraph : Manager<ColourGraph>
{
    // Min number of connections
    [SerializeField]
    private int minConnections = 1;
    // Max number of connections
    [SerializeField]
    private int maxConnections = 4;
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

        createAdjacencies();
    }

    // Recursive method for performing a depth first search on normal adjacency matrix
    private void DFSNormal(bool[] visited, int index)
    {
        visited[index] = true;
        for(int i = 0; i < nodes.Length; i++)
        {
            if (adjacencyMatrix[index, i])
                if (!visited[i])
                    DFSNormal(visited, i);
        }
    }

    // Recursive method for performing a depth first search on an inverted adjacency matrix
    private void DFSInvert(bool[] visited, int index)
    {
        visited[index] = true;
        for (int i = 0; i < nodes.Length; i++)
        {
            if (adjacencyMatrix[i, index])
                if (!visited[i])
                    DFSNormal(visited, i);
        }
    }

    // Check the graph of connections is strongly connected and return true if so
    private bool checkAdjacencies()
    {
        int startingIndex = 0;

        // Refresh visited nodes and perform search
        bool[] visited = new bool[nodes.Length];
        DFSNormal(visited, startingIndex);

        // Check all nodes are connected
        foreach(bool node in visited)
        {
            if (!node)
                return false;
        }

        // Refresh visited nodes and perform search inverted
        visited = new bool[nodes.Length];
        DFSInvert(visited, startingIndex);

        // Check all nodes are connected
        foreach(bool node in visited)
        {
            if (!node)
                return false;
        }

        return true;
    }

    // Create colour adjacencies
    private void createAdjacencies()
    {
        bool[,] emptyMatrix = new bool[nodes.Length, nodes.Length];
        Array.Copy(adjacencyMatrix, emptyMatrix, nodes.Length * nodes.Length);

        // Keep creating a fresh matrix until a strongly connected one is created
        while (true)
        {
            // Initialise the adjacency matrix with random connections
            for (int i = 0; i < nodes.Length; i++)
            {
                int numbConnections = Random.Range(minConnections, maxConnections);
                for (int l = 0; l < numbConnections; l++)
                {
                    int randIndex = Random.Range(0, nodes.Length);
                    if (!adjacencyMatrix[i, randIndex])
                        adjacencyMatrix[i, randIndex] = true;
                    else
                        l--;
                }
            }

            if (checkAdjacencies())
                break;
            else
                Array.Copy(emptyMatrix, adjacencyMatrix, nodes.Length * nodes.Length);
        }

        // Count the incoming nodes for every node
        for (int i = 0; i < nodes.Length; i++)
        {
            int count = 0;
            for (int l = 0; l < nodes.Length; l++)
            {
                if (adjacencyMatrix[l,i])
                    count++;
            }

            nodes[i].setIncoming(count);
        }
    }

    // Get the minimum distance from the source to the destination
    private int getMinDistance(int source, int destination)
    {
        // Shortest path tree and distances
        bool[] spt = new bool[nodes.Length];
        int[] dist = new int[nodes.Length];

        // Initialise all nodes to infinity value except source node
        int inf = int.MaxValue;

        for (int i = 0; i < dist.Length; i++)
            dist[i] = inf;

        dist[source] = 0;

        // Create SPT
        for (int n = 0; n < dist.Length; n++)
        {
            // Find node with smallest distance that hasn't been initialised
            int smallestDist = inf;
            int smallestIndex = -1;
            for (int i = 0; i < dist.Length; i++)
            {
                if (!spt[i] && smallestDist > dist[i])
                {
                    smallestIndex = i;
                    smallestDist = dist[i];
                }
            }

            Debug.Log(smallestIndex);

            spt[smallestIndex] = true;

            // Update distances for all adjacent nodes
            for (int i = 0; i < nodes.Length; i++)
            {
                if (adjacencyMatrix[smallestIndex,i])
                {
                    if (!spt[i])
                    {
                        int newDistance = 1 + dist[smallestIndex];
                        if (newDistance < dist[i])
                            dist[i] = newDistance;
                    }
                }
            }
        }

        return dist[destination];
    }
    
    // Check the path from the source colour to the destination colour
    public ColourResult checkColourPath(string source, string destination)
    {
        // Get indexes of the input colours
        int sourceIndex = -1;
        int destinationIndex = -1;

        foreach(ColourNode node in nodes)
        {
            if (source.Equals(node.getName()))
                sourceIndex = node.getIndex();
            else if (destination.Equals(node.getName()))
                destinationIndex = node.getIndex();
        }

        ColourResult result = new ColourResult() { pathLength = getMinDistance(sourceIndex, destinationIndex), numOfIncoming = nodes[destinationIndex].getIncoming() };

        return result;
    }

    // Get adjacency matrix of colours
    public bool[,] getAdjacencyMatrix()
    {
        return adjacencyMatrix;
    }
}
