
using UnityEngine;


public class PlayerSeeker : SeekerScript
{
    
    private float timer,dist;
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target= FindObjectOfType<ItemMover>().GetComponent<Transform>();
        dist = Random.Range(1f, 1.6f);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance>=dist)
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
            Debug.Log("saldýýr");
        }
    }

}
