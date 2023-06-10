
using UnityEngine;
using System.Collections;
public class PlayerSeeker : SeekerScript, IDamage
{
    private MyBulletPool bulletPool;
    public float fireRate,radius;
    private float timer=0 ;
    public Transform nozzle;
    private UnitSpawn units;
    private Status status;
    public bool dead;
    public LayerMask layer;
    private SelectableUnit unit;
    private void Awake()
    {
        status = GetComponent<Status>();
        unit = GetComponent<SelectableUnit>();
        target = FindObjectOfType<ItemMover>().transform.position;   
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MyBulletPool>();
        
    }

    private void Update()
    {
        if (!dead&&unit.selected==true)
        {

            timer += Time.deltaTime;
            float distance = Vector3.Distance(transform.position, target);

            if (timer > 0.3 && target != null)
            {
                Move(target);
                timer = 0;
                LookAtTarget();
            }

        }

    }
        
    
    public void LoseHealth(int damage)
    {
        status.health -= damage;
        if (status.health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        units.soldiers--;
        Destroy(this.gameObject);
    }
}
