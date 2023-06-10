using UnityEngine;

public class EnemySeeker :SeekerScript,IDamage
{
    private float timer = 0f,dist,distance;
    private bool canMove;
    private BulletPool bulletPool;
    public Transform nozzle;
    public float fireRate;
    private EnemyBarracks enemiesB;
    public float radius;
    public LayerMask layer;
    private Status enemyStatus;
  
    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        enemyStatus=GetComponent<Status>();
        target = GameObject.FindWithTag("Target").transform.position;
        canMove = false;
        bulletPool = FindObjectOfType<BulletPool>();
        dist = 4;
    
        Enemies.Add(this);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);

        bool foundTarget = false; // Flag to track if a valid target is found

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            Transform targetTransform = collider.gameObject.GetComponent<Transform>();

            if (targetTransform != null)
            {
                // Check if the collider has a valid position and is not destroyed
                if (IsValidTarget(collider))
                {
                    target = targetTransform.position;
                    canMove = true;
                    foundTarget = true; 
                    break; 
                }
            }
        }
 
        if (!foundTarget)
        {
            target = transform.position;
            canMove = false;
        }
        timer += Time.deltaTime;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target);
            LookAtTarget();
        }

        if (timer > 0.4 && target != null && distance > dist && canMove)
        {
            Move(target);
            timer = 0;
        }

        if (target != null && distance <= dist&&canMove)
        {
            
            Stop();

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
    private bool IsValidTarget(Collider2D collider)
    {
       
        switch (collider.tag)
        {
            case "Player":
            case "Player2":
            case "Player3":
            case "BarracksIcon":
            case "PowerPlantIcon":
                Transform targetTransform = collider.gameObject.GetComponent<Transform>();

                if (targetTransform != null && targetTransform.gameObject.activeInHierarchy && targetTransform.position != transform.position)
                {
                    return true; 
                }
                break;
        }

        return false; 
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
        Enemies.Remove(this);
     
        enemiesB.enemies--;
        Destroy(this.gameObject);
      
    }
}
