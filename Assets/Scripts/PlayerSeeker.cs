
using UnityEngine;

public class PlayerSeeker : SeekerScript, IDamage
{
    private MyBulletPool bulletPool;
    public float fireRate,radius;
    private float timer=0 ;
    public Transform nozzle;
    private UnitSpawn units;
    private Status status;
    private bool dead;
    public LayerMask layer;
    public bool foundTarget;
    private AudioSource audios;
    public AudioClip clip;
    private float range;
    private void Awake()
    {
        status = GetComponent<Status>();
       
        target = FindObjectOfType<ItemMover>().transform.position;   
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MyBulletPool>();
        audios = FindObjectOfType<AudioSource>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layer);

        //bool foundTarget = false; // Flag to track if a valid target is found

        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    Collider2D collider = colliders[i];
        //    Transform targetTransform = collider.gameObject.GetComponent<Transform>();

        //    if (targetTransform != null)
        //    {
        //        // Check if the collider has a valid position and is not destroyed
        //        if (IsValidTarget(collider))
        //        {
                   
        //            foundTarget = true;
        //            range = Vector3.Distance(transform.position, targetTransform.position);
        //            break;
        //        }
        //    }
        //}
        timer += Time.deltaTime;
        if (!foundTarget&&range<=5)
        {

            if (timer >= fireRate)
            {
                bulletPool.FireBullet2(nozzle.position, nozzle.rotation);
                timer = 0f;
            }
        }

    }
    private bool IsValidTarget(Collider2D collider)
    {

        switch (collider.tag)
        {
            case "EnemyHit":
            case "EnemyBarracks":
         
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
        status.health -= damage;
        if (status.health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        audios.PlayOneShot(clip,Random.Range(.3f,1));
        dead = true;
        units.soldiers--;
        Destroy(this.gameObject);
    }
}
