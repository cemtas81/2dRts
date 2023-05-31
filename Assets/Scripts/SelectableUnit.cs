//using Pathfinding;
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
   
    private bool selected;
   
    public Transform target;
    private SeekerScript seeker;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
     
        seeker = GetComponent<SeekerScript>();
    }
    public void OnSelected()
    {
       
        selectionSprite.gameObject.SetActive(true);
        selected=true;
        //target.gameObject.SetActive(true);
        //seeker.enabled = true;
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
     
        selected=false;
        //target.gameObject.SetActive(false);
        seeker.enabled = false;
    }
   
} 

