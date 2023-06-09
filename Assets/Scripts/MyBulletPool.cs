using UnityEngine;

public class MyBulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 10;

    private GameObject[] bullets;
    private int currentBulletIndex = 0;

    private void Start()
    {
        bullets = new GameObject[poolSize];

       
        for (int i = 0; i < poolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, transform);
            bullets[i].SetActive(false);
        }
    }

    public void FireBullet2(Vector3 position, Quaternion rotation)
    {
       
        bullets[currentBulletIndex].SetActive(true);

        bullets[currentBulletIndex].transform.position = position;
        bullets[currentBulletIndex].transform.rotation = rotation;

        currentBulletIndex = (currentBulletIndex + 1) % poolSize;
    }

}
