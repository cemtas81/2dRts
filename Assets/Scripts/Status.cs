
using UnityEngine;

public class Status : MonoBehaviour
{
    public int initialHealth ;
    [HideInInspector] public int health;
    void Awake()
    {
        health = initialHealth;
    }
}
