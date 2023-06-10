
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileBuildingSystem : MonoBehaviour
{
    public static TileBuildingSystem current;
    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;
    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();
    private BuildingManager temp;
    private Vector3 prevPos;
    public BoundsInt prevArea;
    public GameObject buildCanvas;
   
    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        string tilePath = @"Tiles\";
        tileBases.Add(TileType.Empty, Resources.Load<TileBase>( path:tilePath + "Empty"));
        tileBases.Add(TileType.White, Resources.Load<TileBase>( path:tilePath + "White"));  
        tileBases.Add(TileType.Red, Resources.Load<TileBase>( path:tilePath + "Red"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(path:tilePath + "Green"));
    }
    public void InitializeWithBuilding(GameObject building)
    {
        Vector2 offset = new Vector2(0.5f, -0.5f); // Adjust the offset as needed
        temp = Instantiate(building, Vector2.zero + offset, Quaternion.identity).GetComponent<BuildingManager>();

        FollowBuilding();
        buildCanvas.SetActive( false);
    }
    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea,toClear);
        
    }  
    public void ClearArea2(BoundsInt exArea)
    {

        TileBase[] toClear2 =new TileBase [exArea.size.x*exArea.size.y*exArea.size.z];
        FillTiles(toClear2, TileType.White);
        mainTilemap.SetTilesBlock(exArea, toClear2);

    }
    private void FollowBuilding()
    {
        ClearArea();
        temp.area.position=gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;
        TileBase[] baseArray= GetTilesBlock(buildingArea,mainTilemap);
        int size=baseArray.Length;
        TileBase[] tileArray=new TileBase[size];

        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                FillTiles(tileArray, TileType.Red);

            }
        }
        tempTilemap.SetTilesBlock(buildingArea,tileArray);
        prevArea=buildingArea;
      
    }
    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);

        foreach (var tile in baseArray)
        {
            if (tile != tileBases[TileType.White])
            {
                Debug.Log("Cannot place here!");
                return false;
            }
        }

        Vector2 areaMin = new Vector2(area.min.x, area.min.y);
        Vector2 areaMax = new Vector2(area.max.x, area.max.y);

        Collider2D[] colliders = Physics2D.OverlapAreaAll(areaMin, areaMax);
        foreach (var collider in colliders)
        {
            // Check if the collider belongs to a dynamic character
            if (collider.CompareTag("Player")||collider.CompareTag("Enemy"))
            {
                Debug.Log("Cannot place here - dynamic character present!");
                return false;
            }
        }

        return true;
    }
    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area,TileType.Empty,tempTilemap);
        SetTilesBlock(area, TileType.Green, mainTilemap);
    }
    private void Update()
    {
        if (!temp)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return ;
            }
            if (!temp.Placed)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int cellPos= gridLayout.LocalToCell(touchPos);
                if (prevPos!=cellPos)
                {
                    temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f, .5f, 0));
                    prevPos=cellPos;
                    FollowBuilding();
                }
            }
          
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (temp.CanBePlaced())
            {
                temp.Place();
                buildCanvas.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)&&temp.Placed!=true)
        {
            ClearArea();
            Destroy(temp.gameObject);
            buildCanvas.SetActive(true);
        }
    }   
    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }
    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }
   
    private static TileBase[] GetTilesBlock(BoundsInt area ,Tilemap tilemap)
    {
        TileBase[] array=new TileBase[area.size.x*area.size.y*area.size.z];
        int counter=0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter]=tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

}
