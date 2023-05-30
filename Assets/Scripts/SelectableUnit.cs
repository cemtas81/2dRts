//using Pathfinding;
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
    //private AIDestinationSetter destinationSetter;
    private bool selected;
    private MyAstarPathfinder astarPathfinder;
    public Transform target;
    private Pathfinding2D pathfinding2;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
        //destinationSetter=GetComponent<AIDestinationSetter>(); 
        astarPathfinder=GetComponent<MyAstarPathfinder>(); 
        pathfinding2=GetComponent<Pathfinding2D>();
    }
    public void OnSelected()
    {
        //destinationSetter.enabled=true;
        selectionSprite.gameObject.SetActive(true);
        selected=true;
        target.gameObject.SetActive(true);
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
        //destinationSetter.enabled=false;
        selected=false;
        target.gameObject.SetActive(false); 
    }
    private void Update()
    {
        if (selected == true)
        {
            Pathfind();
        }
        else return;
    }
    public void Pathfind()
    {
        if (target.gameObject.activeInHierarchy == true)
        {
            //astarPathfinder.SetTargetCell(target.position);

            pathfinding2.FindPath(transform.position, target.position);
            //transform.position = pathfinding2.GridOwner.GetComponent<Grid2D>().path[0].worldPosition;
        }

    }
} 

