using System.Collections;

using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
    public bool selected;
    public BoundsInt area;
   
    private PlayerSeeker seeker;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
      
        seeker = GetComponent<PlayerSeeker>();
    }
    public void OnSelected()
    {
        selected = true;
        selectionSprite.gameObject.SetActive(true);
        
        seeker.target = seeker.transform.position;
        seeker.enabled = true;
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
        selected = false;
        //seeker.enabled = false;
        seeker.target = seeker.currenttarget;
    }
 
} 

