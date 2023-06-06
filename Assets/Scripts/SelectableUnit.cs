
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
  
    private bool selected;
    public BoundsInt area;
    private Transform target;
    private PlayerSeeker seeker;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
        target=FindObjectOfType<ItemMover>().GetComponent<Transform>();
        seeker = GetComponent<PlayerSeeker>();
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

