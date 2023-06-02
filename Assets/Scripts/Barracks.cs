
using UnityEngine;

public class Barracks : MonoBehaviour
{
    private Grid grid;
    public CanvasGroup Units;
    private Canvas canvas;
    private Camera cam;
    private void Awake()
    {
        cam=Camera.main;
       
        canvas=GetComponentInParent<Canvas>();
        canvas.worldCamera = cam;
        
    }
    private void Start()
    {
        Units = CanvasGroup.FindObjectOfType<CanvasGroup>();
    }
    public void OpenUnits()
    {
        Units.alpha=1;
    }
    private void OnDisable()
    {
        if (grid!=null)
        {
            grid.CreateGrid();
        }
        
    }
}
