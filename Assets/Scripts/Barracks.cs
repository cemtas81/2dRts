
using UnityEngine;

public class Barracks : MonoBehaviour
{
    private Grid grid;
    public CanvasGroup Units;
    private Canvas canvas;
    private Camera cam;
    [SerializeField]Transform spawnPoint;
    private UnitSpawn spawn;
    private void Awake()
    {
        cam=Camera.main;   
        canvas=GetComponentInParent<Canvas>();
        canvas.worldCamera = cam;
        Units = CanvasGroup.FindObjectOfType<CanvasGroup>();
      

    }
    private void Start()
    {
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
  
}
