using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private float timer = 0f;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Increment the timer
        timer += Time.deltaTime;

        // Deactivate the bullet if it collides with the player or reaches its lifetime
        if (timer >= lifeTime)
        {
            DeactivateBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collides with the player
        if (collision.CompareTag("Player"))
        {
            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        // Reset the timer and deactivate the bullet
        timer = 0f;
        gameObject.SetActive(false);
    }
}

