
using UnityEngine;

public class PlayerSeeker : MonoBehaviour, IDamage
{
    private MyBulletPool bulletPool;
    public float fireRate,radius,dist;
    private float timer=0 ;
    public Transform nozzle;
    private UnitSpawn units;
    private Status status;
    //private bool dead;
    public LayerMask layer;
    public bool foundTarget;
    private AudioSource audios;
    public AudioClip clip;
    private float range;
    private Vector3 target;
    private SeekerScript seeker;
    private void Awake()
    {
        status = GetComponent<Status>();
        seeker = GetComponent<SeekerScript>();
        //target = FindObjectOfType<ItemMover>().transform.position;   
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MyBulletPool>();
        audios = FindObjectOfType<AudioSource>();
        //cam = FindObjectOfType<Camera>();
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
              
                }

                foundTarget = true;
                break;
            }
        }

        if (!foundTarget)
        {
            target = transform.position;
       
            currentTargetValid = false;
        }

        timer += Time.deltaTime;
        if (target != null && currentTargetValid)
        {
            if (target != null)
            {
                Vector3 direction = target - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion q= Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation=Quaternion.RotateTowards(transform.rotation,q,45);
            }
            range = Vector3.Distance(transform.position, target);

            if (range> dist && timer > 0.3f)
            {
              
                timer = 0f;
            }

            if (range <= dist && timer >= fireRate)
            {
                seeker.Stop();
                bulletPool.FireBullet2(nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }
        //else
        //{
        //    //seeker.Stop();
        //}
    }
    private bool IsValidTarget(Collider2D collider, Transform targetTransform)
    {
        string tag = collider.tag;
        return (tag == "EnemyHit" || tag == "EnemyBarracks" )
            && targetTransform != null && targetTransform.gameObject.activeInHierarchy && targetTransform.position != transform.position;
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
        audios.PlayOneShot(clip,Random.Range(.3f,1));
        //dead = true;
        units.soldiers--;
        seeker.StopCoroutines();
        Destroy(this.gameObject);
    
    }
}
