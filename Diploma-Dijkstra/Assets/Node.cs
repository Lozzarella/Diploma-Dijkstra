using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
//A node is the basic building block of pathfinding algorithm. A node represents each space in the map and its relationship to its neighbours
{
    ///<summary>
    /// total cost of shortest path to this node
    /// </summary>

    private float _pathWeight = int.MaxValue; //the weight of the path = the max value or cost 

    public float PathWeight
    {
        get { return _pathWeight; }
        set { _pathWeight = value; }
    }

    //<summary>
    /// following the shortest path, previousNode is the previous step to that path
    /// </summary>

    private Node _previousNode = null;

    public Node PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }
    [SerializeField] private List<Node> _neighbourNodes;

    public List<Node> NeighborNodes
    {
        get
        {
            return _neighbourNodes;
        }
    }

    public void ResetNode()
    {
        _pathWeight = int.MaxValue;
        _previousNode = null;
    }

    public void AddNeighbourNodes(Node node)
    {
        _neighbourNodes.Add(node);
    }

    private void OnDrawGizmos()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, node.transform.position);
        }
    }
    private void Start()
    {
        ResetNode();
        ValidateNeighbours();
    }

    private void OnValidate()
    {
        ValidateNeighbours();
    }

    private void ValidateNeighbours()
    {
        foreach (Node node in _neighbourNodes)
        {
            if (node == null) continue;
            if (!node._neighbourNodes.Contains(this))
            {
                node.AddNeighbourNodes(this);
            }

        }
    }

}
