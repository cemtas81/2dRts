
using UnityEngine;

public class Building1 : MonoBehaviour
{
    private TileBuildingSystem managerb;
  
    public GameObject buildOn;
    private void Awake()
    {
        managerb = FindObjectOfType<TileBuildingSystem>();
       
    }
   
    public void BuildIconOn()
    {
  
        managerb.InitializeWithBuilding(buildOn);
    }
}
