using UnityEngine;

public class PlayerSeeker : SeekerScript, IDamage
{
    private BulletPool bulletPool;
    public float fireRate;
    private float timer=0, dist;
    public Transform nozzle;
    private UnitSpawn units; 
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = FindObjectOfType<ItemMover>().transform;
        dist = Random.Range(0f, 0.5f);
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<BulletPool>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (timer > 0.3 && target != null && distance > dist )
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
                bulletPool.FireBullet(nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }

     
    }

    public void LoseHealth(int damage)
    {
        // Implement the LoseHealth method logic here
    }

    public void Die()
    {
        units.soldiers--;
    }
}
