using UnityEngine;

public class EnemySeeker : SeekerScript, IDamage
{
    private float timer = 0f,dist;
    private bool canMove;
    private BulletPool bulletPool;
    public Transform nozzle;
    public float fireRate;
    private EnemyBarracks enemies;
    public float radius;
    public LayerMask layer;
    private void Awake()
    {
        target = GameObject.FindWithTag("Target").transform;
        canMove = false;
        bulletPool = FindObjectOfType<BulletPool>();
        dist = Random.Range(2f, 3f);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,layer);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];

            if (collider.gameObject.CompareTag("Player"))
            {
                target = collider.gameObject.GetComponent<Transform>();
                canMove = true;
                break;
            }
        } 
        timer += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (timer > 0.4 && target != null && distance > dist && canMove)
        {
            Move(target);
            timer = 0;
        }

        if (target != null && distance <= dist)
        {
            Stop();
            Debug.Log("firee");
            LookAtTarget();

            if (timer >= fireRate)
            {
                bulletPool.FireBullet(nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }

        if (!canMove)
        {
            Stop();
        }
    }

    public void LoseHealth(int damage)
    {
        // Implement the LoseHealth method logic here
    }

    public void Die()
    {
        enemies.enemies--;
    }
}
