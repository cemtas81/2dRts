using UnityEngine;


public class UnitSpawn : MonoBehaviour
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject[] soldierPrefabs;
    public int maxSoldiers, soldiers;
    public MultiObjectPool pool;
    public void SoldierSpawn(int buttonIndex)
    {
        if (buttonIndex==0)
        {
            pool.SpawnFromPool("AISoldierM", m_SpawnTransform.position, Quaternion.identity);
            soldiers++;
        }
        else if (buttonIndex==1)
        {
            pool.SpawnFromPool("AISoldierMfast", m_SpawnTransform.position, Quaternion.identity);
            soldiers++;
        }
        else if (buttonIndex == 2)
        {
            pool.SpawnFromPool("AISoldierMslow", m_SpawnTransform.position, Quaternion.identity);
            soldiers++;
        }
    }
}

