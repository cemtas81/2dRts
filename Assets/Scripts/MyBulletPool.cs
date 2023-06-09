using UnityEngine;

public class MyBulletPool : MonoBehaviour
{
    public GameObject bulletPrefab2;
    public int poolSize = 10;

    private GameObject[] bullets;
    private int currentBulletIndex = 0;

    private void Start()
    {
        bullets = new GameObject[poolSize];

       
        for (int i = 0; i < poolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab2, transform);
            bullets[i].SetActive(false);
        }
    }

    public void FireBullet2(Vector3 position, Quaternion rotation)
    {
       
        bullets[currentBulletIndex].SetActive(true);

        bullets[currentBulletIndex].transform.SetPositionAndRotation(position, rotation);

        currentBulletIndex = (currentBulletIndex + 1) % poolSize;
    }

}
