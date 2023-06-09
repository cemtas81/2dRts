
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
  
   
    public BoundsInt area;
   
    private PlayerSeeker seeker;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
      
        seeker = GetComponent<PlayerSeeker>();
    }
    public void OnSelected()
    {
    
        selectionSprite.gameObject.SetActive(true);
     
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
     
        seeker.enabled = false;
    }
   
} 

