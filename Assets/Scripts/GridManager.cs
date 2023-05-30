using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile gridCellPrefab; // Reference to the grid cell prefab
    public int gridSizeX; // Number of cells in the X-axis
    public int gridSizeY; // Number of cells in the Y-axis
    public float gridCellSize; // Size of each grid cell
    private Dictionary<Vector2, Tile> tiles;
    private void Start()
    {
        CreateGrid();
      
    }

    private void CreateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        Vector3 gridCenter = new Vector3(gridSizeX * 0.5f, gridSizeY * 0.5f, 0f);
        Vector3 cellOffset = new Vector3(0.5f, 0.5f, 0f);
          
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 cellPosition = new Vector3(x, y, 0) * gridCellSize - gridCenter + cellOffset;
                Tile gridCell = Instantiate(gridCellPrefab, cellPosition, Quaternion.identity);
                var issOffset=(x%2==0&&y%2!=0)||(x%2!=0&&y%2==0);
                gridCell.name = $"Tile {x}{y}";
                gridCell.Init(issOffset);
                gridCell.transform.parent = transform;
                tiles[new Vector2(x, y)] = gridCell;
            }
        }
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos,out var tile))
        {
            return tile;
        }
        return null;
    }
}
