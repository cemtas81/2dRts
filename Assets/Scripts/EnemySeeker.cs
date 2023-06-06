
using UnityEngine;

public class EnemySeeker : SeekerScript
{
    float timer3;
    private new void Start()
    {
        target = this.gameObject.transform;
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
        timer3 += Time.deltaTime;

        if (timer3 > 0.30)
        {
            Move(target);
        }
    }
}
