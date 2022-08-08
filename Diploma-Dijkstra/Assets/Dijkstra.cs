using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    private GameObject[] _nodes;
    public Node start;
    public Node end;

    private void Start()
    {
        List<Node> shortestPath = FindShortestPath(start, end);
        Node prevNode = null;
        foreach (Node node in shortestPath)
        {
            if (prevNode != null)
            {
                Debug.DrawLine(node.transform.position + Vector3.up, prevNode.transform.position + Vector3.up, Color.blue, 10f);
            }
            Debug.Log(node.gameObject.name);
            prevNode = node;
        }
    }
    public List<Node> FindShortestPath(Node start, Node end)
    {
        _nodes = GameObject.FindGameObjectsWithTag("Node");
        

        if (DijkstraAlgorithm(start, end))
        {
            List<Node> result = new List<Node>();
            Node node = end; //the first node is end

            do
            {
                result.Insert(0, node);
                node = node.PreviousNode;
            }
            while (node != null);//if the previous node is not null

            return result;
        }
        return null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="node"></param>
    /// <returns></returns>

    private bool DijkstraAlgorithm(Node start, Node end)
    {

        List<Node> unexplored = new List<Node>();
        foreach (GameObject obj in _nodes) //for each gameobject in nodes 
        {
            Node n = obj.GetComponent<Node>();
            if (n == null) continue; //goes back up to the foreach statement, continue with the next loop
            n.ResetNode();
            unexplored.Add(n); //fill up unexplored nodes with nodes

        }

        if (!unexplored.Contains(start) && !unexplored.Contains(end))
        {
            return false;
        }

        start.PathWeight = 0;//the first node is by default 0 - we start here and then we loop through every node and tick them off
        while (unexplored.Count > 0)
        {
            //order based on path
            unexplored.Sort((x, y) => x.PathWeight.CompareTo(y.PathWeight));//Sort sorts the list and needs to pass a comparison - how to compare the two nodes - whats bigger/smaller based on the weight of the path, x and y are the two nodes
            
            //current is the current shortest path possibility
            Node current = unexplored[0];

            if (current == end)
            {
                break;
            }

            unexplored.RemoveAt(0); //removing the object at the index

            foreach(Node neighbourNode in current.NeighborNodes)//grabbing all of its neighbour nodes
            {
                
                if (!unexplored.Contains(neighbourNode)) continue;
               
                float distance = Vector3.Distance(neighbourNode.transform.position, current.transform.position);//current to the neighbour

                distance += current.PathWeight;

                if (distance < neighbourNode.PathWeight)
                {
                    neighbourNode.PathWeight = distance;
                    neighbourNode.PreviousNode = current;
                }

                
            }
        }


        return true;
    }
}
