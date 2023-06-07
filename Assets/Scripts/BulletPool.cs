using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;

    private GameObject[] bullets;
    private int currentBulletIndex = 0;

    private void Start()
    {
        bullets = new GameObject[poolSize];

        // Instantiate bullets and disable them initially
        for (int i = 0; i < poolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, transform);
            bullets[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        // Activate the next available bullet
        bullets[currentBulletIndex].SetActive(true);

        // Set bullet position and rotation to match the gun's position and rotation
        bullets[currentBulletIndex].transform.position = transform.position;
        bullets[currentBulletIndex].transform.rotation = transform.rotation;

        // Move to the next bullet in the pool
        currentBulletIndex = (currentBulletIndex + 1) % poolSize;
    }
}
