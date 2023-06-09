
using UnityEngine;

public class PlayerSeeker : SeekerScript, IDamage
{
    private MyBulletPool bulletPool;
    public float fireRate;
    private float timer=0, dist;
    public Transform nozzle;
    private UnitSpawn units;
    private Status status;
    public bool dead;
    private void Awake()
    {
        status = GetComponent<Status>();

        target = FindObjectOfType<ItemMover>().transform.position;
        dist = Random.Range(0f, 0.5f);
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MyBulletPool>();
    }

    private void Update()
    {
        if (!dead)
        {
            timer += Time.deltaTime;
            float distance = Vector3.Distance(transform.position, target);

            if (timer > 0.3 && target != null && distance > dist)
            {
                Move(target);
                timer = 0;
            }

            if (target != null && distance <= dist)
            {
                Stop();
                Debug.Log("saldýr");
                LookAtTarget();

                if (timer >= fireRate)
                {
                    bulletPool.FireBullet2(nozzle.position, nozzle.rotation);
                    timer = 0f;
                }
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
