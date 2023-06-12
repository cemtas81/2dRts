
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
    public bool selected;
    public BoundsInt area;
   
    private SeekerScript seeker;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
      
        seeker = GetComponent<SeekerScript>();
    }
    public void OnSelected()
    {
        selected = true;
        selectionSprite.gameObject.SetActive(true);
     
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
        selected = false;
        seeker.enabled = false;
    }
   
} 

