using System.Collections.Generic;
using UnityEngine;

public class Flocking2D : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public float neighborRadius = 3.0f;
    public float separationWeight = 1.0f;
    public float alignmentWeight = 1.0f;
    public float cohesionWeight = 1.0f;

    private List<Transform> neighbors = new List<Transform>();

    void Update()
    {
        FindNeighbors();

        Vector2 separation = CalculateSeparation() * separationWeight;
        Vector2 alignment = CalculateAlignment() * alignmentWeight;
        Vector2 cohesion = CalculateCohesion() * cohesionWeight;

        Vector2 moveDirection = (separation + alignment + cohesion).normalized;
        Move(moveDirection);
    }

    void FindNeighbors()
    {
        neighbors.Clear();
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, neighborRadius);
        foreach (Collider2D collider in nearbyColliders)
        {
            if (collider.transform != transform)
            {
                neighbors.Add(collider.transform);
            }
        }
    }

    Vector2 CalculateSeparation()
    {
        Vector2 separation = Vector2.zero;
        foreach (Transform neighbor in neighbors)
        {
            Vector2 toNeighbor = transform.position - neighbor.position;
            separation += toNeighbor.normalized / toNeighbor.magnitude;
        }
        return separation;
    }

    Vector2 CalculateAlignment()
    {
        Vector2 alignment = Vector2.zero;
        foreach (Transform neighbor in neighbors)
        {
            alignment += (Vector2)neighbor.transform.up;
        }
        return alignment / neighbors.Count;
    }

    Vector2 CalculateCohesion()
    {
        Vector2 center = Vector2.zero;
        foreach (Transform neighbor in neighbors)
        {
            center += (Vector2)neighbor.position;
        }
        center /= neighbors.Count;
        return (center - (Vector2)transform.position).normalized;
    }

    void Move(Vector2 direction)
    {
        transform.up = Vector2.Lerp(transform.up, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
