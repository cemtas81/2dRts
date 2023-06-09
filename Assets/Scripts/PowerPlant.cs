
using UnityEngine;

public class PowerPlant : MonoBehaviour,IDamage
{
    
    private Status pStatus;
    private BuildingManager pPlant;
    private Collider2D pCollider;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        pPlant=GetComponentInParent<BuildingManager>();
        sprite = GetComponent<SpriteRenderer>();    
        pCollider=GetComponent<Collider2D>();
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
        sprite.enabled = false;
        pCollider.enabled = false;
        pPlant.Demolition();

    }
  
}
