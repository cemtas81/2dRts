
using UnityEngine;


public class MyAstarPathfinder : MonoBehaviour
{
    public GridManager gridManager;
    public Vector2Int startCell;
    public Vector2Int targetCell;
    public float movementSpeed;
    public float cellSize;
    private Vector2 currentVelocity;
    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        // Example usage:
        //startCell = new Vector2Int(0, 0); // Set the start position
        //targetCell = new Vector2Int(7, 7); // Set the initial target position
        //List<Vector2Int> path = FindPath(startCell, targetCell);
        // Use the path for further operations
    }

    public void SetTargetCell(Vector2 targetPosition)
    {

        //// Convert the target position to grid coordinates
        //float cellSize = gridManager.gridCellPrefab.transform.localScale.x;
        //targetCell = new Vector2Int(
        //    Mathf.FloorToInt(targetPosition.x / cellSize),
        //    Mathf.FloorToInt(targetPosition.y / cellSize)
        //);

        //// Calculate the world position of the target cell
        //Vector3 targetWorldPosition = new Vector3(
        //    targetCell.x * cellSize,
        //    targetCell.y * cellSize,
        //    0f
        //);

        //// Move the item to the target position
        var step = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        //// Call the FindPath method again to recalculate the path
        //List<Vector2Int> path = FindPath(startCell, targetCell);
        //// Use the updated path for further operations
    }

    //    private List<Vector2Int> FindPath(Vector2Int startCell, Vector2Int targetCell)
    //    {
    //        // Convert the start and target cell positions to grid coordinates
    //        Vector2Int startGridPos = new Vector2Int(startCell.x, startCell.y);
    //        Vector2Int targetGridPos = new Vector2Int(targetCell.x, targetCell.y);

    //        // Initialize the open and closed lists
    //        HashSet<Vector2Int> openList = new HashSet<Vector2Int>();
    //        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();
    //        openList.Add(startGridPos);

    //        // Create a dictionary to keep track of the parent cell for each grid cell
    //        Dictionary<Vector2Int, Vector2Int> parentCells = new Dictionary<Vector2Int, Vector2Int>();

    //        // Create a dictionary to store the G score for each grid cell
    //        Dictionary<Vector2Int, int> gScores = new Dictionary<Vector2Int, int>();
    //        gScores[startGridPos] = 0;

    //        // Create a dictionary to store the F score for each grid cell
    //        Dictionary<Vector2Int, int> fScores = new Dictionary<Vector2Int, int>();
    //        fScores[startGridPos] = Heuristic(startGridPos, targetGridPos);

    //        while (openList.Count > 0)
    //        {
    //            // Find the cell with the lowest F score in the open list
    //            Vector2Int currentCell = FindLowestFScore(openList, fScores);

    //            // If the current cell is the target cell, we have found the path
    //            if (currentCell == targetGridPos)
    //                return ReconstructPath(parentCells, currentCell);

    //            // Move the current cell from the open list to the closed list
    //            openList.Remove(currentCell);
    //            closedList.Add(currentCell);

    //            // Get the neighboring cells
    //            List<Vector2Int> neighbors = GetNeighborCells(currentCell);

    //            foreach (Vector2Int neighborCell in neighbors)
    //            {
    //                // Skip the neighbor if it is already in the closed list
    //                if (closedList.Contains(neighborCell))
    //                    continue;

    //                // Calculate the tentative G score for the neighbor
    //                int tentativeGScore = gScores[currentCell] + 1;

    //                // If the neighbor is not in the open list, add it
    //                if (!openList.Contains(neighborCell))
    //                    openList.Add(neighborCell);
    //                else if (tentativeGScore >= gScores[neighborCell])
    //                    continue; // This is not a better path

    //                // Update the parent cell and G score for the neighbor
    //                parentCells[neighborCell] = currentCell;
    //                gScores[neighborCell] = tentativeGScore;
    //                fScores[neighborCell] = gScores[neighborCell] + Heuristic(neighborCell, targetGridPos);
    //            }
    //        }

    //        // No path found
    //        return null;
    //    }


    //    private Vector2Int FindLowestFScore(HashSet<Vector2Int> openList, Dictionary<Vector2Int, int> fScores)
    //    {
    //        int lowestFScore = int.MaxValue;
    //        Vector2Int lowestCell = Vector2Int.zero;

    //        foreach (Vector2Int cell in openList)
    //        {
    //            if (fScores[cell] < lowestFScore)
    //            {
    //                lowestFScore = fScores[cell];
    //                lowestCell = cell;
    //            }
    //        }

    //        return lowestCell;
    //    }

    //    private List<Vector2Int> GetNeighborCells(Vector2Int cell)
    //    {
    //        List<Vector2Int> neighbors = new List<Vector2Int>();

    //        Vector2Int[] directions =
    //        {
    //            new Vector2Int(-1, 0), // Left
    //            new Vector2Int(1, 0), // Right
    //            new Vector2Int(0, -1), // Down
    //            new Vector2Int(0, 1) // Up
    //        };

    //        foreach (Vector2Int direction in directions)
    //        {
    //            Vector2Int neighbor = cell + direction;

    //            // Check if the neighbor is within the grid bounds
    //            if (neighbor.x >= 0 && neighbor.x < gridManager.gridSizeX &&
    //                neighbor.y >= 0 && neighbor.y < gridManager.gridSizeY)
    //            {
    //                // Add the valid neighbor to the list
    //                neighbors.Add(neighbor);
    //            }
    //        }

    //        return neighbors;
    //    }

    //    private int Heuristic(Vector2Int cell, Vector2Int targetCell)
    //    {
    //        // Manhattan distance heuristic
    //        return Mathf.Abs(cell.x - targetCell.x) + Mathf.Abs(cell.y - targetCell.y);
    //    }

    //    private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> parentCells, Vector2Int currentCell)
    //    {
    //        List<Vector2Int> path = new List<Vector2Int>();

    //        while (parentCells.ContainsKey(currentCell))
    //        {
    //            path.Add(currentCell);
    //            currentCell = parentCells[currentCell];
    //        }

    //        path.Reverse();
    //        return path;
    //    }
}

