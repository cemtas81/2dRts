using UnityEngine;

public class Bullet : MonoBehaviour
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

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
            case "Player":
                PlayerSeeker player = collision.GetComponent<PlayerSeeker>();
                if (player != null)
                {
                    player.LoseHealth(10);
                }
                DeactivateBullet();
                break;
            case "Player2":
                PlayerSeeker player2 = collision.GetComponent<PlayerSeeker>();
                if (player2 != null)
                {
                    player2.LoseHealth(5);
                }
                DeactivateBullet();
                break;
            case "Player3":
                PlayerSeeker player3 = collision.GetComponent<PlayerSeeker>();
                if (player3 != null)
                {
                    player3.LoseHealth(2);
                }
                DeactivateBullet();
                break;
            case "BarracksIcon":
                Barracks barracks = collision.GetComponentInChildren<Barracks>();
                if (barracks != null)
                {
                    barracks.LoseHealth(10);
                }
                DeactivateBullet();
                break;
            case "PowerPlantIcon":
                PowerPlant pPlant = collision.GetComponent<PowerPlant>();
                if (pPlant != null)
                {
                    pPlant.LoseHealth(10);
                }
                DeactivateBullet();
                break;
        }
    }


    private void DeactivateBullet()
    {
        // Reset the timer and deactivate the bullet
        timer = 0f;
        gameObject.SetActive(false);
    }
}

