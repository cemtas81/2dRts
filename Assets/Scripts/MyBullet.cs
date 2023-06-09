using UnityEngine;

public class MyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private float timer = 0f;

    private void Start()
    {
        timer = 0f;
    }
    private void Update()
    {
     
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            DeactivateBullet2();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                EnemySeeker enemy = collision.GetComponentInParent<EnemySeeker>();
                if (enemy != null)
                {
                    enemy.LoseHealth(10);
                }
                DeactivateBullet2();
                break;
            case "EnemyBarracks":
                EnemyBarracks eBarracks = collision.GetComponentInParent<EnemyBarracks>();
                if (eBarracks != null)
                {
              
                    eBarracks.LoseHealth(10);
                }
                DeactivateBullet2();
                break;
            
        }
    }

    private void DeactivateBullet2()
    {
      
        timer = 0f;
        gameObject.SetActive(false);
    }
}

