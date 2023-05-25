
using System.Collections.Generic;
using UnityEngine;

public class MyAstarPathfinder : MonoBehaviour
{
    // Represents a single node in the grid
    private class Node
    {
        public bool IsWalkable;
        public Vector2 Position;
        public Node Parent;
        public int GCost; // Cost from the start node to this node
        public int HCost; // Estimated cost from this node to the target node

        public int FCost => GCost + HCost; // Total cost of the node (GCost + HCost)

        public Node(bool isWalkable, Vector2 position)
        {
            IsWalkable = isWalkable;
            Position = position;
        }
    }

    private Node[,] grid;
    private int gridSizeX;
    private int gridSizeY;

    private void Start()
    {
        // Call this method to generate the grid
        GenerateGrid();
    }

    // Generates the grid for pathfinding
    private void GenerateGrid()
    {
        // TODO: Implement your grid generation logic here
        // Create nodes for each cell in the grid and set their walkable status

        // Example code assuming you have a 10x10 grid
        gridSizeX = 10;
        gridSizeY = 10;
        grid = new Node[gridSizeX, gridSizeY];

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Assuming (0, 0) is the bottom-left corner of the grid
                Vector2 position = new Vector2(x, y);
                bool isWalkable = true; // Set the walkable status based on your game logic

                grid[x, y] = new Node(isWalkable, position);
            }
        }
    }

    // Performs the A* pathfinding algorithm
    public List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        Node startNode = NodeFromWorldPosition(start);
        Node targetNode = NodeFromWorldPosition(target);

        // Create open and closed sets
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            // Find the node with the lowest FCost in the open set
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // Found the target node, reconstruct the path and return it
            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            // Check neighboring nodes
            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, targetNode);
                    neighbor.Parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        // No path found
        return null;
    }

    // Retraces the path from the target node to the start node
    private List<Vector2> RetracePath(Node startNode, Node targetNode)
    {
        List<Vector2> path = new List<Vector2>();
        Node currentNode = targetNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    // Returns the distance between two nodes
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs((int)nodeA.Position.x - (int)nodeB.Position.x);
        int distanceY = Mathf.Abs((int)nodeA.Position.y - (int)nodeB.Position.y);

        // Diagonal movement cost (assuming both horizontal and vertical movements have a cost of 1)
        int diagonalCost = 14;
        int straightCost = 10;

        // Return diagonal distance if there is a diagonal movement, otherwise return straight distance
        if (distanceX > distanceY)
        {
            return diagonalCost * distanceY + straightCost * (distanceX - distanceY);
        }
        else
        {
            return diagonalCost * distanceX + straightCost * (distanceY - distanceX);
        }
    }

    // Returns the neighboring nodes of a given node
    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        // Define the possible movement directions (assuming 8-directional movement)
        Vector2[] directions =
        {
            new Vector2(-1, 0),  // Left
            new Vector2(1, 0),   // Right
            new Vector2(0, -1),  // Down
            new Vector2(0, 1),   // Up
            new Vector2(-1, -1), // Bottom-left
            new Vector2(-1, 1),  // Top-left
            new Vector2(1, -1),  // Bottom-right
            new Vector2(1, 1)    // Top-right
        };

        foreach (Vector2 direction in directions)
        {
            int neighborX = (int)node.Position.x + (int)direction.x;
            int neighborY = (int)node.Position.y + (int)direction.y;

            if (neighborX >= 0 && neighborX < gridSizeX && neighborY >= 0 && neighborY < gridSizeY)
            {
                neighbors.Add(grid[neighborX, neighborY]);
            }
        }

        return neighbors;
    }

    // Converts a world position to the corresponding node in the grid
    private Node NodeFromWorldPosition(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        return grid[x, y];
    }
}
