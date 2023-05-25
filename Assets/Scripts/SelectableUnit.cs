using Pathfinding;
using UnityEngine;

public class SelectableUnit : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer selectionSprite;
    private AIDestinationSetter destinationSetter;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this); 
        destinationSetter=GetComponent<AIDestinationSetter>();  
    }
    public void OnSelected()
    {
        destinationSetter.enabled=true;
        selectionSprite.gameObject.SetActive(true);
    }
    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
        destinationSetter.enabled=false;    
    }
}
