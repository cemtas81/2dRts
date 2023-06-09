
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
    // Start is called before the first frame update
    void Start()
    {
        enemyBStatus = GetComponent<Status>(); 
        StartCoroutine(Area());
        InvokeRepeating(nameof(EnemySpawn), 1, spawn);
    }
 
    IEnumerator Area()
    {
        yield return new WaitForSeconds(0.1f);
        Place();
       
    }
    void EnemySpawn()
    {
        if (enemies<maxEnemy)
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
            Die();
        }

    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
