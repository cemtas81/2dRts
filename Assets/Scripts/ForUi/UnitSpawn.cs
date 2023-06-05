
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject soldier;
  
    public void SoldierSpawn() 
    {
        Instantiate(soldier,m_SpawnTransform.position,Quaternion.identity);
    }
}
