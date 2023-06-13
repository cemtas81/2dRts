
using UnityEngine;
using System.Collections;


public class EnemyBarracks :BuildingManager,IDamage
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject soldier;
    public float spawn;
    public int enemies;
    public int maxEnemy;
    private Status enemyBStatus;
    private RtsMover rts;
    private bool canProduce;
    // Start is called before the first frame update
    void Start()
    {
        rts = FindObjectOfType<RtsMover>();
        enemyBStatus = GetComponent<Status>(); 
        StartCoroutine(Area());
        InvokeRepeating(nameof(EnemySpawn), 1, spawn);
        rts.enemyBase.Add(this.gameObject);
        canProduce = true;
    }
 
    IEnumerator Area()
    {
        yield return new WaitForSeconds(0.1f);
        Place();
       
    }
    void EnemySpawn()
    {
        if (enemies<maxEnemy&&canProduce)
        {
            Instantiate(soldier, m_SpawnTransform.position, Quaternion.identity);
            enemies++;
        }
       
    }
    public void LoseHealth(int damage)
    {
        enemyBStatus.health-=damage;
        if (enemyBStatus.health<=0)
        {
            GetComponent<Collider>().enabled = false;
            rts.enemyBase.Remove(this.gameObject);
            Die();
            canProduce = false;
        }

    }
    public void Die()
    {
        GetComponentInChildren<Collider>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        rts.CheckEnemyBases();
        Demolition();
    }
}
