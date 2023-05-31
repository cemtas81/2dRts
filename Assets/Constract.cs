using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constract : MonoBehaviour
{
    private Grid grid;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        grid = FindObjectOfType<Grid>();
        grid.CreateGrid();
    }
}
