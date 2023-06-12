
using UnityEngine;

public class PlayerSeeker : MonoBehaviour, IDamage
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
       
        //target = FindObjectOfType<ItemMover>().transform.position;   
        units=FindObjectOfType<UnitSpawn>();
        bulletPool = FindObjectOfType<MyBulletPool>();
        audios = FindObjectOfType<AudioSource>();
        //cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {

        timer += Time.deltaTime;
        if (foundTarget)
        {
            //LookAtTarget();
            if (timer >= fireRate)
            {
                bulletPool.FireBullet2(nozzle.position, nozzle.rotation);
                timer = 0f;
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
        audios.PlayOneShot(clip,Random.Range(.3f,1));
        dead = true;
        units.soldiers--;
        Destroy(this.gameObject);
    }
}
