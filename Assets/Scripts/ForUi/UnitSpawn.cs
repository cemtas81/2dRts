using UnityEngine;


public class UnitSpawn : MonoBehaviour
{
    public Transform m_SpawnTransform;
    [SerializeField] GameObject[] soldierPrefabs;
    public int maxSoldiers, soldiers;

    public void SoldierSpawn(int buttonIndex)
    {
        if (buttonIndex < soldierPrefabs.Length)
        {
            if (soldiers < maxSoldiers)
            {
                GameObject prefabToSpawn = soldierPrefabs[buttonIndex];
                Instantiate(prefabToSpawn, m_SpawnTransform.position, Quaternion.identity);

                soldiers++;
            }
        }
    }
}

