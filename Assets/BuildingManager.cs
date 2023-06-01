using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Grid gridManager;
    public GameObject buildingPrefab;
    public int buildingSizeX;
    public int buildingSizeY;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z=0;
            AddBuilding(mousePosition);
        }
    }

    void AddBuilding(Vector3 position)
    {
        // Convert the position to grid coordinates
        Node node = gridManager.NodeFromWorldPoint(position);

        // Check if the building can be placed on the grid
        if (!CanPlaceBuilding(node))
        {
            Debug.Log("Cannot place building here!");
            return;
        }

        // Instantiate the building prefab
        GameObject building = Instantiate(buildingPrefab, position, Quaternion.identity);

        // Mark the grid nodes as occupied by the building
        MarkNodesAsOccupied(node, buildingSizeX, buildingSizeY);

        // Set the appropriate size for the building (optional)
        building.transform.localScale = new Vector3(buildingSizeX, buildingSizeY, 1f);
    }

    bool CanPlaceBuilding(Node node)
    {
        // Check if the starting node and its surrounding nodes are walkable and not already occupied
        for (int x = node.gridX; x < node.gridX + buildingSizeX; x++)
        {
            for (int y = node.gridY; y < node.gridY + buildingSizeY; y++)
            {
                if (x >= gridManager.gridSizeX || y >= gridManager.gridSizeY)
                {
                    // Building exceeds grid bounds
                    return false;
                }

                Node currentNode = gridManager.grid[x, y];

                if (!currentNode.isWalkable || currentNode.isOccupied)
                {
                    // Building cannot be placed here
                    return false;
                }
            }
        }

        return true;
    }

    void MarkNodesAsOccupied(Node node, int sizeX, int sizeY)
    {
        // Mark the nodes within the building's size as occupied
        for (int x = node.gridX; x < node.gridX + sizeX; x++)
        {
            for (int y = node.gridY; y < node.gridY + sizeY; y++)
            {
                gridManager.grid[x, y].isOccupied = true;
            }
        }
    }
}


