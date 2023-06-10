
using UnityEngine;

public class PowerPlant : MonoBehaviour,IDamage
{
    
    private Status pStatus;
    private BuildingManager pPlant;
    // Start is called before the first frame update
    void Start()
    {
        pPlant=GetComponentInParent<BuildingManager>(); 
        pStatus=GetComponent<Status>();
   
    }

    public void LoseHealth(int damage)
    {
        pStatus.health -= damage;
        if (pStatus.health <= 0)
        {
            
            Die();
        }

    }
   
    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        pPlant.Demolition();

    }
  
}
