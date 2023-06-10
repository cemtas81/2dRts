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
    private RtsMover rts;
    private void Awake()
    {
        rts = FindObjectOfType<RtsMover>();
        enemyStatus=GetComponent<Status>();
        target = GameObject.FindWithTag("Target").transform.position;
        canMove = false;
        bulletPool = FindObjectOfType<BulletPool>();
        dist = 4;
        rts.enemies.Add(this);
        Enemies.Add(this);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius,layer);
 
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
           
            switch (collider.tag)
            {
                case "Player":
                    target = collider.gameObject.GetComponent<Transform>().position;
                    canMove = true;
                    break;
                case "Player2":
                    target = collider.gameObject.GetComponent<Transform>().position;
                    canMove = true;
                    break;
                case "Player3":
                    target = collider.gameObject.GetComponent<Transform>().position;
                    canMove = true;
                    break;
                case "BarracksIcon":
                    target = collider.gameObject.GetComponent<Transform>().position;
                    canMove = true;
                    break;
                case "PowerPlantIcon":
                    target = collider.gameObject.GetComponent<Transform>().position;
                    canMove = true;
                    break;
                case "Untagged":
                    target=transform.position;
                    canMove = false;
                    break;
            }
        }
        timer += Time.deltaTime;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target);
        }

        if (timer > 0.4 && target != null && distance > dist && canMove)
        {
            Move(target);
            timer = 0;
        }

        if (target != null && distance <= dist&&canMove)
        {
            LookAtTarget();
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
        rts.enemies.Remove(this);
        enemiesB.enemies--;
        Destroy(this.gameObject);
      
    }
}
