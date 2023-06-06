
using UnityEngine;


public class PlayerSeeker : SeekerScript
{
    
    float timer2;
    private new void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = FindObjectOfType<ItemMover>().GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        timer2 += Time.deltaTime;

        if (timer2 > 0.30)
        {
            Move(target);
        }
    }

}
