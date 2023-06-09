
using UnityEngine;

public class PowerPlant : MonoBehaviour,IDamage
{
    private Status pStatus;
    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(this.gameObject);
    }
}
