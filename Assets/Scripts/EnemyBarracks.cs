
using UnityEngine;
using System.Collections;
public class EnemyBarracks : MonoBehaviour,IDamage
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject soldier;
    public BoundsInt area;
    private Grid grid;
    public float spawn;
    // Start is called before the first frame update
    void Start()
    {    
        grid = FindObjectOfType<Grid>();    
        StartCoroutine(Area());
        InvokeRepeating("EnemySpawn", 1, spawn);
    }
    IEnumerator Area()
    {
        yield return new WaitForSeconds(0.1f);
        TileBuildingSystem.current.TakeArea(area);       
        grid.CreateGrid();
    }
    void EnemySpawn()
    {
        Instantiate(soldier, m_SpawnTransform.position, Quaternion.identity);
    }
    public void LoseHealth(int damage)
    {
       

    }
    public void Die()
    {

    }
}
