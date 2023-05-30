public class GridCell
{
    public bool walkable; // Indicates if the cell is walkable or blocked
    public int gridX; // X index of the cell in the grid
    public int gridY; // Y index of the cell in the grid
    public int gCost; // Cost from the start cell to this cell
    public int hCost; // Heuristic cost from this cell to the target cell
    public int fCost => gCost + hCost; // Total cost (gCost + hCost)
    public GridCell parent; // Parent cell used for path construction

    public GridCell(bool walkable, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}

