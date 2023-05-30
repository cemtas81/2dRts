using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding2D : MonoBehaviour
{

    public Transform seeker, target;
    Grid2D grid;
    Node2D seekerNode, targetNode;
    public GameObject GridOwner;


    void Start()
    {
        //Instantiate grid
        grid = GridOwner.GetComponent<Grid2D>();
    }


    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        //get player and target position in grid coords
        seekerNode = grid.NodeFromWorldPoint(startPos);
        targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node2D> openSet = new List<Node2D>();
        HashSet<Node2D> closedSet = new HashSet<Node2D>();
        openSet.Add(seekerNode);
        
        //calculates path for pathfinding
        while (openSet.Count > 0)
        {
          
            //iterates through openSet and finds lowest FCost
            Node2D node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost <= node.FCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            //If target found, retrace path
            if (node == targetNode)
            {
                RetracePath(seekerNode, targetNode);
                return;
            
            }
            
            //adds neighbor nodes to openSet
            foreach (Node2D neighbour in grid.GetNeighbors(node))
            {
                if (neighbour.obstacle || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    //reverses calculated path so first node is closest to seeker
    void RetracePath(Node2D startNode, Node2D endNode)
    {
        List<Node2D> path = new List<Node2D>();
        Node2D currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        StartCoroutine(MoveAlongPath(path));
    }
    IEnumerator MoveAlongPath(List<Node2D> path)
    {
        foreach (Node2D node in path)
        {
            // Convert the node's grid coordinates to world position using the Grid2D script
            Vector3 targetPosition = GridOwner.GetComponent<Grid2D>().Grid[node.GridX, node.GridY].worldPosition;

            // Move the seeker object towards the target position
            while (Vector3.Distance(seeker.transform.position, targetPosition) > 0.05f)
            {
                // Adjust the speed and movement logic according to your requirements
                //float moveSpeed = 5f;
                //seeker.transform.position = Vector3.MoveTowards(seeker.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                seeker.transform.position = seeker.GetComponent<Pathfinding2D>().GridOwner.GetComponent<Grid2D>().path[0].worldPosition;
                yield return null;
            }
        }

    }
    //gets distance between 2 nodes for calculating cost
    int GetDistance(Node2D nodeA, Node2D nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}