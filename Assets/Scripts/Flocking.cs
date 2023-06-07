using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float neighborRadius = 2f;
    public float separationDistance = 1f;

    private List<GameObject> flock;

    private void Start()
    {
        flock = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    private void Update()
    {
        Vector2 separationForce = Vector2.zero;
        Vector2 alignmentForce = Vector2.zero;
        Vector2 cohesionForce = Vector2.zero;

        int neighborCount = 0;

        foreach (GameObject boid in flock)
        {
            if (boid != gameObject)
            {
                float distance = Vector2.Distance(transform.position, boid.transform.position);

                if (distance <= neighborRadius)
                {
                    alignmentForce += (Vector2)boid.transform.up;
                    cohesionForce += (Vector2)boid.transform.position;
                    neighborCount++;

                    if (distance <= separationDistance)
                    {
                        separationForce += (Vector2)(transform.position - boid.transform.position);
                    }
                }
            }
        }

        if (neighborCount > 0)
        {
            alignmentForce /= neighborCount;
            alignmentForce = alignmentForce.normalized;

            cohesionForce /= neighborCount;
            cohesionForce = cohesionForce - (Vector2)transform.position;
            cohesionForce = cohesionForce.normalized;
        }

        Vector2 direction = (separationForce + alignmentForce + cohesionForce).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 newPosition = transform.position + (Vector3)(direction * speed * Time.deltaTime);
        transform.position = newPosition;
    }
}



