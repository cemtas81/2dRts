
using UnityEngine;

public class EnemySeeker : SeekerScript
{
    private float timer;
    void Awake()
    {
     
        target=GameObject.FindWithTag("Target").transform;
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject.transform;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.4&&target!=null)
        {
            Move(target);
            timer = 0;
        }
    }
}
