﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Node m_startNode;
    Node m_goalNode;
    Graph m_graph;
    GraphView m_graphView;
    Queue<Node> m_frontierNodes;
    List<Node>
        m_exploreNodes,
        m_pathNodes;

    public Color
        startColor = Color.green,
        goalColor = Color.red,
        frontierColor = Color.magenta,
        exploredColor = Color.gray,
        pathColor = Color.cyan;

    public void Init(Graph graph, GraphView graphView, Node start, Node goal)
    {
        if (start == null || goal == null || graph == null || graphView == null)
        {
            Debug.LogWarning("PATHFINDER Init error: missing component(s)!");
            return;
        }
        if (start.nodeType == NodeType.Blocked || goal.nodeType == NodeType.Blocked)
        {
            Debug.LogWarning("PATHFINDER Init error: start and goal nodes must be unblocked!");
            return;
        }
        m_graph = graph;
        m_graphView = graphView;
        m_startNode = start;
        m_goalNode = goal;
        NodeView startNodeView = graphView.nodeViews[start.xIndex, start.yIndex];
        if (startNodeView != null)
        {
            startNodeView.ColorNode(startColor);
        }
        NodeView goallNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex];
        if (goallNodeView != null)
        {
            goallNodeView.ColorNode(goalColor);
        }
        m_frontierNodes = new Queue<Node>();
        m_frontierNodes.Enqueue(start);
        m_exploreNodes = new List<Node>();
        m_pathNodes = new List<Node>();
        for (int x = 0; x < m_graph.Width; x++)
        {
            for (int y = 0; y < m_graph.Height; y++)
            {
                m_graph.nodes[x, y].Reset();
            }
        }
    }
}
