
using UnityEngine;

public class EnemySeeker : SeekerScript,IDamage
{
    private float timer, dist;
    private bool canMove;
    void Awake()
    {     
        target = GameObject.FindWithTag("Target").transform;
        canMove = false;
        dist = Random.Range(2f,2.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject.GetComponent<Transform>();
            canMove=true;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
  
        if (timer > 0.4 && target != null && distance > dist && canMove == true)
        {
            Move(target);
            timer = 0;
        }

        if (target != null && distance <= dist)
        {
            Stop();
            Debug.Log("firee");
            timer = 0;

        }
        if (canMove == false)
        {
            Stop();
        }

    }
    public void LoseHealth(int damage)
    {


    }
    public void Die()
    {

    }
}

