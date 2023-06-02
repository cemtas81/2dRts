
using UnityEngine;

public class Building1 : MonoBehaviour
{
    private TileBuildingSystem managerb;
    private SpriteRenderer sprite1;
    public GameObject buildOn;
    private void Awake()
    {
        managerb = FindObjectOfType<TileBuildingSystem>();
        //buildOn = GameObject.FindWithTag("BarracksIcon");
        //sprite1= buildOn.GetComponent<SpriteRenderer>();    
    }
   
   public void BuildIconOn()
    {
        managerb.enabled = true;
        //sprite1.enabled = true;
        //managerb.enabled = true;
        managerb.InitializeWithBuilding(buildOn);
    }
}
