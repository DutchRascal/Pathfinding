using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        pathColor = Color.cyan,
        arrowColor = new Color32(216, 216, 216, 255),
        highlightColor = new Color32(255, 255, 128, 255);
    public bool isComplete = false;
    int m_iterations = 0;

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

        ShowColors(graphView, start, goal);

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

        isComplete = false;
        m_iterations = 0;
    }

    void ShowColors()
    {
        ShowColors(m_graphView, m_startNode, m_goalNode);
    }

    private void ShowColors(GraphView graphView, Node start, Node goal)
    {
        if (graphView == null || start == null || goal == null)
        {
            return;
        }

        if (m_frontierNodes != null)
        {
            graphView.ColorNodes(m_frontierNodes.ToList(), frontierColor);
        }

        if (m_exploreNodes != null)
        {
            graphView.ColorNodes(m_exploreNodes, exploredColor);
        }

        if (m_pathNodes != null && m_pathNodes.Count > 0)
        {
            graphView.ColorNodes(m_pathNodes, pathColor);
        }

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
    }

    public IEnumerator SearchRoutine(float timeStep = 0.1f)
    {
        yield return null;

        while (!isComplete)
        {
            if (m_frontierNodes.Count > 0)
            {
                Node currentNode = m_frontierNodes.Dequeue();
                m_iterations++;

                if (!m_exploreNodes.Contains(currentNode))
                {
                    m_exploreNodes.Add(currentNode);

                }
                ExpandFrontier(currentNode);

                if (m_frontierNodes.Contains(m_goalNode))
                {
                    m_pathNodes = GetpathNodes(m_goalNode);
                }

                ShowColors();

                if (m_graphView != null)
                {
                    m_graphView.ShowNodeArrows(m_frontierNodes.ToList(), arrowColor);

                    if (m_frontierNodes.Contains(m_goalNode))
                    {
                        m_graphView.ShowNodeArrows(m_pathNodes, highlightColor);
                    }
                }

                yield return new WaitForSeconds(timeStep);
            }
            else
            {
                isComplete = true;
            }
        }
    }

    void ExpandFrontier(Node node)
    {
        if (node != null)
        {
            for (int i = 0; i < node.neighbors.Count; i++)
            {
                if (!m_exploreNodes.Contains(node.neighbors[i]) && !m_frontierNodes.Contains(node.neighbors[i]))
                {
                    node.neighbors[i].previous = node;
                    m_frontierNodes.Enqueue(node.neighbors[i]);
                }
            }
        }
    }

    List<Node> GetpathNodes(Node endNode)
    {
        List<Node> path = new List<Node>();
        if (endNode == null)
        {
            return path;
        }
        path.Add(endNode);
        Node currentNode = endNode.previous;
        while (currentNode != null)
        {
            path.Insert(0, currentNode);

            currentNode = currentNode.previous;
        }
        return path;
    }
}
