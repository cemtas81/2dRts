using System.Collections.Generic;
using UnityEngine;

public class EnemySeeker : SeekerScript
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
        if (distance >= dist)
        {
            if (timer > 0.30)
            {
                Move(target);
                timer = 0;
            }
        }
     
        else
        {
            Stop();
            timer = 0;
            Debug.Log("firee");
        }
        //if (timer > 0.4 && target != null &&distance>2&&canMove==true)
        //{
        //    Move(target);
        //    timer = 0;
        //}

        //if (target != null && distance <= 2)
        //{
        //    Stop();
        //    Debug.Log("firee");
        //    timer = 0;
           
        //}
        //if (canMove==false)
        //{
        //    Stop();
        //}
        
       
    }
}

