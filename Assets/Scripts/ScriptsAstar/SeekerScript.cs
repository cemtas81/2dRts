
using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class SeekerScript : MonoBehaviour
{
	public Transform target;
	public float speed = 1;
    //public float rotationSpeed = 1;
    Vector2[] path;
	int targetIndex;
    //public float timer;
    private Vector3 currenttarget;
    public SpriteRenderer spriteRenderer;
    public float angle;
    private Quaternion targetRotation;
    public void Start()
    {
  
        currenttarget = target.position;
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
        //if (currenttarget.x < transform.position.x)
        //    FlipSprite(true); // Flip sprite to face left
        //else
        //    FlipSprite(false); // Flip sprite to face right
    }

    public void FlipSprite(bool faceLeft)
    {
        //Flip the sprite(if its not topdown) based on the faceLeft parameter
        spriteRenderer.flipX = faceLeft;
    }
 
    public void Move(Transform target)
    {
        //timer = 0;

        //  if the target position have change already
        if ((target.position != currenttarget))
        {
            //Debug.Log ("Path changed to " + target.position);
            currenttarget = target.position;
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            //Flip the sprite based on the target's position relative to the seeker
            //if (currenttarget.x < transform.position.x)
            //    FlipSprite(true); // Flip sprite to face left
            //else
            //    FlipSprite(false); // Flip sprite to face right
        }
    }
    public void Stop()
    {
        
        StopCoroutine("FollowPath");
    }
    public void OnPathFound(Vector2[] newPath, bool pathSuccessful)
    {
		if (pathSuccessful)
        {
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

    IEnumerator FollowPath()
    {
        targetIndex = 0;
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = new Vector2[0];
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            // Calculate the desired movement direction towards the current waypoint
            Vector3 targetWaypointDirection = currentWaypoint - transform.position;
            Vector3 movementDirection = targetWaypointDirection.normalized;

            // Check for collisions using a raycast in the movement direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, movementDirection.magnitude);

            if (hit.collider != null)
            {
                // If a collision is detected, adjust the movement direction to avoid the obstacle
                movementDirection = Vector3.Reflect(movementDirection, hit.normal);
            }

            // Move towards the next waypoint using the adjusted movement direction
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movementDirection, Vector2.up), rotationSpeed * Time.fixedDeltaTime);
            angle =math.atan2 (targetWaypointDirection.y, targetWaypointDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }
    public void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}