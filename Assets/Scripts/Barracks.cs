
using UnityEngine;

public class Barracks : MonoBehaviour,IDamage
{
  
    private CanvasGroup Units;
    public Transform spawnPoint;
    private UnitSpawn spawn;
  
    private void Start()
    {
        Units = CanvasGroup.FindObjectOfType<CanvasGroup>();
        spawn = FindObjectOfType<UnitSpawn>();
        Units.alpha = 0;
        Units.interactable = false;
    
    }
  
    public void OpenUnits()
    {

        spawn.m_SpawnTransform = spawnPoint;
        Units.alpha=1;
        Units.interactable = true;
    }
    public void CloseB()
    {
        Units.alpha = 0;
        Units.interactable = false;
    }
    public void LoseHealth(int damage)
    {


    }
    public void Die()
    {

    }
}
