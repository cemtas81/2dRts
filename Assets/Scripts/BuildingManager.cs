
using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area,areaTemp;
    private MyGrid grid;
    public BoxCollider coll;
 
    private void OnEnable()
    {       
        grid = FindObjectOfType<MyGrid>();
        coll=GetComponent<BoxCollider>(); 
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
        areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        TileBuildingSystem.current.TakeArea(areaTemp);
        grid.CreateGrid();
        Debug.Log("Placed");
        coll.enabled = true;
    }
    public void Demolition()
    {
    
        TileBuildingSystem.current.ClearArea2(area);
        coll.enabled = false;
        StartCoroutine(Demo());
    }
    IEnumerator Demo()
    {
        yield return new WaitForSeconds(.2f);
        grid.CreateGrid();
        Destroy(this.gameObject);
      
    }
}


