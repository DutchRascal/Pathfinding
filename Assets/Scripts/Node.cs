﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Open = 0,
    Blocked = 1,
    LightTerrain = 2,
    MediumTerrain = 3,
    HeavyTerrain = 4
}

public class Node : IComparable<Node>
{
    public NodeType nodeType = NodeType.Open;
    public int
        xIndex = -1,
        yIndex = -1;
    public float priority;
    public Vector3 position;
    public List<Node> neighbors = new List<Node>();
    public Node previous = null;
    public float distanceTraveled = Mathf.Infinity;


    public Node(int xIndex, int yIndex, NodeType nodeType)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.nodeType = nodeType;
    }

    public void Reset()
    {
        previous = null;
    }

    public int CompareTo(Node other)
    {
        if (this.priority < other.priority)
        {
            return -1;
        }
        else if (this.priority > other.priority)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
