
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject soldier;
    public int maxSoldiers;
    public int soldiers;
    public void SoldierSpawn() 
    {
        if (soldiers<maxSoldiers)
        {
            Instantiate(soldier, m_SpawnTransform.position, Quaternion.identity);

            soldiers++;
        }
  
    }
}
