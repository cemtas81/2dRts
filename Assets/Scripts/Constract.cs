
using UnityEngine;

public class Constract : MonoBehaviour
{
    private Grid grid;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
        if (grid!=null)
        {
            grid.CreateGrid();
        }
        
    }
    private void OnDisable()
    {
        if (grid!=null)
        {
            grid.CreateGrid();
        }
        
    }
}
