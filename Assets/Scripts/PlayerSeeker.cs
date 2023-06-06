
using UnityEngine;


public class PlayerSeeker : SeekerScript
{

    private float timer;
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target= FindObjectOfType<ItemMover>().GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.30)
        {
            Move(target);
            timer = 0;
        }
    }

}
