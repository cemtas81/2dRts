using UnityEngine;

public class EnemySeeker : SeekerScript, IDamage
{
    private float timer = 0f,dist,distance;
    private bool canMove;
    private BulletPool bulletPool;
    public Transform nozzle;
    public float fireRate;
    private EnemyBarracks enemies;
    public float radius;
    public LayerMask layer;
    private Status enemyStatus;
    private void Awake()
    {
        enemyStatus=GetComponent<Status>();
        target = GameObject.FindWithTag("Target").transform.position;
        canMove = false;
        bulletPool = FindObjectOfType<BulletPool>();
        dist = Random.Range(3f, 4f);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,layer);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];

            if (collider.gameObject.CompareTag("Player"))
            {
                target = collider.gameObject.GetComponent<Transform>().position;
                canMove = true;
                break;
            }
            else if (collider.gameObject.CompareTag("Player2"))
            {
                target = collider.gameObject.GetComponent<Transform>().position;
                canMove = true;
                break;
            } 
            else if (collider.gameObject.CompareTag("Player3"))
            {
                target = collider.gameObject.GetComponent<Transform>().position;
                canMove = true;
                break;
            }
            else if (collider.gameObject.CompareTag("BarracksIcon"))
            {
                target = collider.gameObject.GetComponent<Transform>().position;
                canMove = true;
                break;
            }
            else if (collider.gameObject.CompareTag("PowerPlantIcon"))
            {
                target = collider.gameObject.GetComponent<Transform>().position;
                canMove = true;
                break;
            }
        } 
        timer += Time.deltaTime;
        if (target!=null)
        {
           distance = Vector3.Distance(transform.position, target);
        }
  
        if (timer > 0.4 && target != null && distance > dist && canMove)
        {
            Move(target);
            timer = 0;
        }

        if (target != null && distance <= dist)
        {
            Stop();
          
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
        enemyStatus.health -= damage;
        if (enemyStatus.health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        enemies.enemies--;
        Destroy(this.gameObject);
    }
}
