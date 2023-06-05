
using UnityEngine;


public class BuildingManager : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private Grid grid;
   
    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
    }
    public bool CanBePlaced()
    {
        Vector3Int positionInt = TileBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position=positionInt;
        if (TileBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }
        return false;
    }
    public void Place()
    {
        Vector3Int positionInt = TileBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        TileBuildingSystem.current.TakeArea(areaTemp);
        grid.CreateGrid();
        Debug.Log("Placed");

    }
   
}

