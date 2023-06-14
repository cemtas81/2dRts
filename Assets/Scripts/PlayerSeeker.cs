
using UnityEngine;

public class PlayerSeeker : SeekerScript, IDamage
{
    private MultiObjectPool bulletPool;
    public float fireRate,radius,dist;
    private float timer=0,range ;
    public Transform nozzle;
    private UnitSpawn units;
    private Status status;
    //private bool dead;
    public LayerMask layer;
    public bool locked;
    private AudioSource audios;
    public AudioClip clip;

 
    private void Awake()
    {
        status = GetComponent<Status>();
       
        units =FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MultiObjectPool>();
        audios = FindObjectOfType<AudioSource>();
        
    }
    private void OnEnable()
    {
        target = transform.position;
    }
  
    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);
        bool foundTarget2 = false;
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

                foundTarget2 = true;
                break;
            }
        }

        if (!foundTarget2)
        {
            target = transform.position;
            locked = false;
            currentTargetValid = false;
        }
        if (target == null)
        {
            locked = false;
            return; // Exit the Update method if the target is no longer valid
        }
        timer += Time.deltaTime;
        if (target != null && currentTargetValid)
        {

            Vector3 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 45);

            range = Vector3.Distance(transform.position, target);

            if (range > dist && timer > 0.3f)
            {
                timer = 0f;
            }

            if (range <= dist && timer >= fireRate && locked)
            {
                Stop();
                bulletPool.SpawnFromPool("MyBullet", nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }
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
        StopCoroutines();
        this.gameObject.SetActive(false);
    
    }
}
