
using System.Collections.Generic;

public class SelectionManager
{
    private static SelectionManager instance;
    public static SelectionManager Instance
    {
        get 
        {
            if (instance==null)
            {
                instance = new SelectionManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public HashSet<SelectableUnit> SelectedUnits= new HashSet<SelectableUnit>();
    public List<SelectableUnit> AvailableUnits= new List<SelectableUnit>();
    //private SelectionManager()
    //{
       
    //}
    public void Select(SelectableUnit Unit)      
    {
        Unit.OnSelected();
        SelectedUnits.Add(Unit);    
    }
    public void Deselect(SelectableUnit Unit)
    {
        Unit.OnDeselected();  
        SelectedUnits.Remove(Unit); 
    }
    public void DeselectAll()
    {
        foreach (SelectableUnit Unit in SelectedUnits)
        {
            Unit.OnDeselected();
        }
        SelectedUnits.Clear();  
    }
    public bool IsSelected(SelectableUnit Unit)
    {
        return SelectedUnits.Contains(Unit);
    }
}
