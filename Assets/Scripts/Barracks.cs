
using UnityEngine;

public class Barracks : MonoBehaviour,IDamage
{
  
    private CanvasGroup Units;
    public Transform spawnPoint;
    private UnitSpawn spawn;
    private Status bStatus;

    private void Start()
    {
        Units = CanvasGroup.FindObjectOfType<CanvasGroup>();
        spawn = FindObjectOfType<UnitSpawn>();
        Units.alpha = 0;
        Units.interactable = false;
        bStatus=GetComponent<Status>();
    }
  
    public void OpenUnits()
    { 
        spawn.m_SpawnTransform = spawnPoint;
        Units.alpha = 1;
        Units.interactable = true;
    }
    public void CloseB()
    {
        Units.alpha = 0;
        Units.interactable = false;
    }
    public void LoseHealth(int damage)
    {
        bStatus.health -= damage;
        if (bStatus.health <= 0)
        {
            Die();
        }

    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
