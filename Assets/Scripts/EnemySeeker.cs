using UnityEngine;

public class EnemySeeker : SeekerScript, IDamage
{
    private float timer = 0f;
    public float dist = 4f;
    private bool canMove;
    private BulletPool bulletPool;
    public Transform nozzle;
    public float fireRate;
    public float radius;
    public LayerMask layer;
    private Status enemyStatus;

    private void Awake()
    {
        enemyStatus = GetComponent<Status>();
        bulletPool = FindObjectOfType<BulletPool>();
        canMove = false;
     
        Enemies.Add(this);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        bool foundTarget = false;
        bool currentTargetValid = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D collider = colliders[i];
            Transform targetTransform = collider.gameObject.GetComponent<Transform>();

            if (IsValidTarget(collider, targetTransform))
            {
                if (targetTransform.position == target)
                {
                    currentTargetValid = true;
                }
                else
                {
                    target = targetTransform.position;
                    currentTargetValid = true;
                    canMove = true;
                }

                foundTarget = true;
                break;
            }
        }

        if (!foundTarget)
        {
            target = transform.position;
            canMove = false;
            currentTargetValid = false;
        }

        timer += Time.deltaTime;
        if (target != null && currentTargetValid)
        {
            LookAtTarget();
            float distance = Vector3.Distance(transform.position, target);

            if (canMove && distance > dist && timer > 0.4f)
            {
                Move(target);
                timer = 0f;
            }

            if (canMove && distance <= dist && timer >= fireRate)
            {
                Stop();
                bulletPool.FireBullet(nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }
        else
        {
            Stop();
        }
    }


    private bool IsValidTarget(Collider2D collider, Transform targetTransform)
    {
        string tag = collider.tag;
        return (tag == "Player" || tag == "Player2" || tag == "Player3" || tag == "BarracksIcon" || tag == "PowerPlantIcon")
            && targetTransform != null && targetTransform.gameObject.activeInHierarchy && targetTransform.position != transform.position;
    }

    public void LoseHealth(int damage)
    {
        enemyStatus.health -= damage;
        if (enemyStatus.health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Enemies.Remove(this);
        Destroy(gameObject);
    }
}
